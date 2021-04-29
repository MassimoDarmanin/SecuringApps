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
    [Authorize]
    public class TaskController : Controller
    {
        private readonly SecuringAppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TaskController> _logger;

        public TaskController(SecuringAppDbContext db, UserManager<ApplicationUser> userManager, ILogger<TaskController> logger)
        {
            _db = db;
            _userManager = userManager;
            _logger = logger;
        }

        public string Message { get; set; }

        public IActionResult Index()
        {
            string userName = _userManager.GetUserName(User);
            Message = "User: " + userName + $"\nTask Index visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);

            IEnumerable<TaskModel> taskList = _db.Tasks;
            return View(taskList);
        }

        [Authorize(Roles = "Teacher")]
        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskModel task)
        {
            string userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                _db.Tasks.Add(task);
                task.UserId = Guid.Parse(userId);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(task);            
        }
    }
}
