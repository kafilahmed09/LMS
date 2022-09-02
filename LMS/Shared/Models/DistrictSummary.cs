using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.Models
{
    public class DistrictSummary
    {
        [Key]
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int Enroll { get; set; }
    }
}
