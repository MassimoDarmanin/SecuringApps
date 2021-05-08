using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecuringApps.ActionFilters;
using SecuringApps.Data;
using SecuringApps.Models;
using SecuringApps.Services;
using SecuringApps.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        //private readonly SecuringAppDbContext _db;
        private readonly ICommentServices _commentServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentServices commentServices, UserManager<ApplicationUser> userManager, ILogger<CommentController> logger)
        {
            _commentServices = commentServices;
            _userManager = userManager;
            _logger = logger;
        }

        public string Message { get; set; }

        [TeacherStudentFilter]
        public IActionResult Index(string id)
        {
            string userName = _userManager.GetUserName(User);

            Message = "User: " + userName + $"\nFile Index visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);

            string idDecrypted = Encryption.SymmetricDecrypt(id);
            Guid guid = new Guid(idDecrypted);

            ViewBag.SelectedFile = idDecrypted;

            var list = _commentServices.GetAllComments();
            var listIndex = list.Where(f => f.FileId == idDecrypted);
            //IQueryable<FileModel> fileList = _db.Files.Where(f => f.TaskId == guid);
            //return View(fileList);
            return View(listIndex);

            //IEnumerable<CommentModel> cmtList = _db.Comments.Where(c => c.FileId == id);
            //return View(cmtList);           
        }

        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string id, CommentModel cmt)
        {
            string userId = _userManager.GetUserId(User);
            string userEmail = _userManager.GetUserName(User);
            string fileId = id;
            try
            {
                //_taskServices.Add(task, Guid.Parse(userId));
                if (ModelState.IsValid)
                {
                    //_db.Tasks.Add(task);
                    //task.UserId = Guid.Parse(userId);
                    //_db.SaveChanges();

                    _commentServices.AddComment(cmt, userId, userEmail, fileId);

                    return RedirectToAction("Index", "Task");
                }
                return View(cmt);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = "Error while posting comment." });
            }
        }
    }
}
