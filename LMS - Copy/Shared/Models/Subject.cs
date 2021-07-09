
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Models
{
   public class Subject
    {
        [Key]        
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public UploadedFile File { get; set; }        
        [ForeignKey("Grade")]
        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; }
    }
}
