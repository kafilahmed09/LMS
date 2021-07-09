

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LMS.Shared.Models
{
   public class Tehsil
    {
        [Key]
        public int TehsilId { get; set; }
        [Required]
        [ForeignKey("District")]                
        public int DistrictId { get; set; }
        [Required]        
        public string Name { get; set; }       
        public virtual District District { get; set; }
       
    }
}
