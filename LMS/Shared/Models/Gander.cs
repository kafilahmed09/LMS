using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.Models
{
    public class Gander
    {
        [Key]
        public int GanderlId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
