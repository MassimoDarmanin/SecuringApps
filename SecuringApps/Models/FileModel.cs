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
    public class FileModel
    {

        public Guid Id { get; set; }

        public string FileName { get; set; }


        public DateTime? DateUploaded { get; set; }

        public string FileType { get; set; }
        public string Extension { get; set; }

        [NotMapped]
        public IFormFile Attachment { get; set; }

        //FK
        public virtual TaskModel Tasks { get; set; }

        //[Required]

        //public virtual Guid TaskId { get; set; }

        public string TaskId { get; set; }

        //[Required]
        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public string Signature { get; set; }
    }
}
