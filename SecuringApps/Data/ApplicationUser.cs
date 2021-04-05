using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Data
{
    public class ApplicationUser : IdentityUser
    {
        //[Required]
        public String Name { get; set; }

        //[Required]
        public String Surname { get; set; }

        public bool isTeacher { get; set; }
    }
}
