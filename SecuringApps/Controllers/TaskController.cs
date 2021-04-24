using Microsoft.AspNetCore.Mvc;
using SecuringApps.Data;
using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Controllers
{
    public class TaskController : Controller
    {
        private readonly SecuringAppDbContext _db;

        public TaskController(SecuringAppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TaskModel> taskList = _db.Tasks;
            return View(taskList);
        }
    }
}
