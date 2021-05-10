using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecuringApps.ActionFilters;
using SecuringApps.Data;
using SecuringApps.Models;
using SecuringApps.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        //private readonly SecuringAppDbContext _db;
        private readonly ITaskServices _taskServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TaskController> _logger;
        private IWebHostEnvironment _env;

        public TaskController(ITaskServices taskServices, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, ILogger<TaskController> logger)
        {
            //_db = db;
            _userManager = userManager;
            _logger = logger;
            _taskServices = taskServices;
            _env = env;
        }

        public string Message { get; set; }

        public IActionResult Index()
        {
            string userName = _userManager.GetUserName(User);
            Message = "User: " + userName + $"\nTask Index visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);

            var list = _taskServices.GetAllFiles();
            return View(list);
            //IEnumerable<TaskModel> taskList = _db.Tasks;
            //return View(taskList);
            //return View();
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
        [TeacherStudentFilter]
        public IActionResult Create(TaskModel task)
        {
            string userId = _userManager.GetUserId(User);
            string userName = _userManager.GetUserName(User);
            try
            {
                //_taskServices.Add(task, Guid.Parse(userId));
                if (ModelState.IsValid)
                {
                    //_db.Tasks.Add(task);
                    //task.UserId = Guid.Parse(userId);
                    //_db.SaveChanges();

                    _taskServices.Add(task,userId);
                    Message = "User: " + userName + $"\nTask created at {DateTime.UtcNow.ToLongTimeString()}";
                    _logger.LogInformation(Message);

                    return RedirectToAction("Index");
                }
                return View(task);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = "Error while saving File." });
            }
                       
        }
    }
}
