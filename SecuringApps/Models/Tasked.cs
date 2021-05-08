using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Models
{
    public class Tasked
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [ForeignKey("AspNetUsers")]
        //[Required]
        public Guid UserId { get; set; }

        //public bool Disable { get; set; } //refresh db - run commands
    }
}
