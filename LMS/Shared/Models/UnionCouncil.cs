
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Models
{
    public class UnionCouncil
    {
        [Key]
        public int UnionCouncilId { get; set; }
        public string Name { get; set; }
        [Required]
        [ForeignKey("Tehsil")]                
        public int TehsilId { get; set; }   
        public virtual Tehsil Tehsil { get; set; }
    }
}
