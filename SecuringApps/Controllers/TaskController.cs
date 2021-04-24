﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public TaskController(SecuringAppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
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
            if (ModelState.IsValid)
            {
                _db.Tasks.Add(task);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(task);            
        }
    }
}
