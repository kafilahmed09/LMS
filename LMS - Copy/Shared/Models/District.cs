
using System.ComponentModel.DataAnnotations;


namespace LMS.Shared.Models
{
   public  class District
    {
        [Key]
        public int DistrictId { get; set; }
        [Required]        
        public string Name { get; set; }
    }
}

