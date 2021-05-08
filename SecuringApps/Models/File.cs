using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Models
{
    public class File
    {
        [Key]
        public Guid Id { get; set; }

        //[Required]
        [DisplayName("File Name")]
        public string FileName { get; set; }

        //[Required]
        [DisplayName("Upload Date")]
        public DateTime? DateUploaded { get; set; }

        public string FileType { get; set; }
        public string Extension { get; set; }

        //[Required]
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile Attachment { get; set; }

        //FK
        public virtual TaskModel Tasks { get; set; }

        //[Required]
        //[ForeignKey("Id")]
        //public virtual string TaskId { get; set; }
        public string TaskId { get; set; }

        [ForeignKey("AspNetUsers")]
        //[Required]
        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public string Signature { get; set; }

    }
}
