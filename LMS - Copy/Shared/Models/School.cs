
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LMS.Shared.Models
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }
        public string Name { get; set; }
        [Required]
        [ForeignKey("SchoolLevel")]
        public int SchoolLevelId { get; set; }
        [Required]
        [ForeignKey("Gander")]
        public int GanderId { get; set; }
        public string BEMIS { get; set; }
        [Required]
        [ForeignKey("UnionCouncil")]
        public int UnionCouncilId { get; set; }
        public virtual UnionCouncil UnionCouncil { get; set; }
        public virtual SchoolLevel SchoolLevel { get; set; }
        public virtual Gander Gander { get; set; }
    }
}
