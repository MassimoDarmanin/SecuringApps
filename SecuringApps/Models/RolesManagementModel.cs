using Microsoft.AspNetCore.Identity;
using SecuringApps.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Models
{
    public class RolesManagementModel
    {
        public List<IdentityRole> Roles { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
