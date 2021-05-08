using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }

        public string CommentText { get; set; }

        //FK
        public virtual FileModel Files { get; set; }
        public virtual Guid FileId { get; set; }        
        

        public Guid UserId { get; set; }
    }
}
