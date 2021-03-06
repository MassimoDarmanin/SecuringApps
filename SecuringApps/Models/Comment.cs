using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CommentText { get; set; }

        //FK
        public virtual FileModel Files { get; set; }

        //[Required]
        //[ForeignKey("Id")]
        public string FileId { get; set; }


        [ForeignKey("AspNetUsers")]
        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserEmail { get; set; }
    }
}
