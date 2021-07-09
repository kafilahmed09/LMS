using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using LMS.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using LMS.Server.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using LMS.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            [Display(Name = "District")]
            public int DistrictId { get; set; }
            [Required]
            [Display(Name = "School")]
            public int SchoolId { get; set; }
            /*[Required]
            [DataType(DataType.Text)]
            [Display(Name = "User Name")]
            public string UserName { get; set; }*/

            /*[Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }*/
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ViewData["ddlDistrict"] = new SelectList(_context.Districts, "DistrictId", "Name");
            ViewData["ddlSchool"] = new SelectList(_context.Schools.Include(a => a.UnionCouncil.Tehsil).Where(a => a.UnionCouncil.Tehsil.DistrictId == 1 && a.SchoolLevelId == 4), "SchoolId", "Name");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }
        public async Task<JsonResult> OnGetSchools(int id)
        {
            List<School> schoolList = new List<School>();
            schoolList = await _context.Schools.Include(a => a.UnionCouncil.Tehsil).Where(a => a.UnionCouncil.Tehsil.DistrictId == id).ToListAsync();
            //schoolList.Insert(0, new School { SchoolId = 0, Name = "Select" });
            return new JsonResult(new SelectList(schoolList, "SchoolId", "Name"));
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var firstpart = Input.Email.Substring(0, Input.Email.IndexOf('@'));

                var user = new ApplicationUser { UserName = firstpart, Email = Input.Email, DistrictId = Input.DistrictId, SchoolId = Input.SchoolId };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewData["ddlDistrict"] = new SelectList(_context.Districts, "DistrictId", "Name", Input.DistrictId);
            ViewData["ddlSchool"] = new SelectList(_context.Schools.Include(a => a.UnionCouncil.Tehsil).Where(a => a.UnionCouncil.Tehsil.DistrictId == Input.DistrictId), "SchoolId", "Name", Input.SchoolId);
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
