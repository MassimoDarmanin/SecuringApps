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
    public class CommentController : Controller
    {
        private readonly SecuringAppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CommentController> _logger;

        public CommentController(SecuringAppDbContext db, UserManager<ApplicationUser> userManager, ILogger<CommentController> logger)
        {
            _db = db;
            _userManager = userManager;
            _logger = logger;
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
