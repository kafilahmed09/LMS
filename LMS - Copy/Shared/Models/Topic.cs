using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }
        public string Name { get; set; }
        public string SerialNo { get; set; }        
        [ForeignKey("Video")]
        public int VideoId { get; set; }
        public virtual Video Video { get; set; }
    }
}
