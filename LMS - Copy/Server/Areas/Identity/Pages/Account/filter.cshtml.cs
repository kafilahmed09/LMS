using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Server.Data;
using LMS.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LMS.Server.Areas.Identity.Pages.Account
{
    public class filterModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public filterModel(ApplicationDbContext context)
        {           
            _context = context;            
        }
        public JsonResult OnGet(int id, int schoolLevel)
        {
            List<School> schoolList = new List<School>();
            schoolList = _context.Schools.Include(a => a.UnionCouncil.Tehsil).Where(a => a.UnionCouncil.Tehsil.DistrictId == id && a.SchoolLevelId == schoolLevel).ToList();
            var output = new SelectList(schoolList, "SchoolId", "Name");
            var output2 = new JsonResult(output);
            return output2;
        }
    }
}
