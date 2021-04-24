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
    public class CommentController : Controller
    {
        private readonly SecuringAppDbContext _db;

        public CommentController(SecuringAppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(Guid id)
        {
            IEnumerable<CommentModel> cmtList = _db.Comments.Where(c => c.FileId == id);
            return View(cmtList);
        }

        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CommentModel cmt)
        {
            if (ModelState.IsValid)
            {
                _db.Comments.Add(cmt);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(cmt);
        }
    }
}