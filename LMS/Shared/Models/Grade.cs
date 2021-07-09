

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Models
{
    public class Grade
    {
        [Key]        
        public int GradeId { get; set; }
        [Required]
        [ForeignKey("SchoolLevel")]
        public int SchoolLevelId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ImagePath { get; set; }
        public virtual SchoolLevel SchoolLevel { get; set; }
    }
}
