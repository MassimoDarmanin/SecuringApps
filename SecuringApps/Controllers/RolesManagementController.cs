using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Controllers
{
    [Authorize(Roles = "ADMIN, TEACHER")]

    public class RolesManagementController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllocateRoles(string role, string user)
        {
            return View();
        }

        public IActionResult DeallocateRoles(string role, string user)
        {
            return View();
        }
    }
}
