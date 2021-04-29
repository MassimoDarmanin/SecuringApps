using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecuringApps.Data;
using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Controllers
{
    //[Authorize(Roles = "TEACHER")]
    [Authorize(Roles = "Teacher")]

    public class RolesManagementController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RolesManagementController> _logger;

        public RolesManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<RolesManagementController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            RolesManagementModel model = new RolesManagementModel();
            model.Roles = _roleManager.Roles.ToList();
            model.Users = _userManager.Users.ToList();

            return View();
        }

        public async Task<IActionResult> AllocateRolesAsync(string role, string user, string btnName)
        {
            var returnedUser = await _userManager.FindByEmailAsync(user);

            if (btnName == "Allocate")
            {
                if(returnedUser != null)
                {
                    await _userManager.AddToRoleAsync(returnedUser, role); //adds role to user
                    TempData["message"] = "User Successlly Allocated.";
                }
                else
                {
                    TempData["error"] = "User not found.";
                }
                
            }
            else if (btnName == "Deallocate")
            {
                if (returnedUser != null)
                {
                    await _userManager.RemoveFromRoleAsync(returnedUser, role);//removes role to user
                    TempData["message"] = "User Successlly Allocated.";
                }
                else
                {
                    TempData["error"] = "User not found.";
                }
            }
            else
            {
                TempData["error"] = "Please use buttons provided.";
            }

            return RedirectToAction("Index");
        }

        /*
        public IActionResult DeallocateRoles(string role, string user)
        {
            return View();
        }
        */
    }
}
