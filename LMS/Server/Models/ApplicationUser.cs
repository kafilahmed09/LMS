using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int DistrictId { get; set; }
        public int SchoolId { get; set; }
    }
}
