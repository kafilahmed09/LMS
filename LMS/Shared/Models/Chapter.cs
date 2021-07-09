
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Models
{
    public class Chapter
    {
        [Key]    
        public int ChapterId { get; set; }
        public string Name { get; set; }
        public int SerialNo { get; set; }        
        public string ImagePath { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

    }
}
