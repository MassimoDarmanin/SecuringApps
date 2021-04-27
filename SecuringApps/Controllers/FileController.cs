using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecuringApps.Data;
using SecuringApps.Models;
using SecuringApps.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly SecuringAppDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public FileController(SecuringAppDbContext db, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        /*public IActionResult Index(Guid id)
        {
            IEnumerable<FileModel> fileList = _db.Files.Where(f => f.TaskId == id);
            return View(fileList);
        }*/
        public IActionResult Index(string id)
        {
            string idDecrypted = Encryption.SymmetricDecrypt(id);
            Guid guid = new Guid(idDecrypted);

            IEnumerable<FileModel> fileList = _db.Files.Where(f => f.TaskId == guid);
            return View(fileList);
        }

        /*[HttpGet]
        public IActionResult Index(Guid id)
        {
            var model = _db.Files.Where(p => p.TaskId == id).FirstOrDefault();
            return View(model);
        }*/

        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Guid id, FileModel files)
        {
            string userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                //save image to wwwroot/attachment
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(files.Attachment.FileName);
                string extention = Path.GetExtension(files.Attachment.FileName);

                //files.FileName = fileName = fileName + DateTime.Now.ToString("yymmdd");
                string path = Path.Combine(wwwRootPath + "/Attachment/", fileName) + extention;

                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                   await files.Attachment.CopyToAsync(fileStream);
                }

                //insert record
                _db.Files.Add(files);
                files.UserId = Guid.Parse(userId);
                files.TaskId = id;
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(files);
        }
    }
}


/*
 * var net = new System.Net.WebClient();
            var data = net.DownloadData(submission.FileURL);
            var content = new System.IO.MemoryStream(data);
            var contentType = "APPLICATION/pdf";
            var fileName = "Submission.pdf";
            return File(content, contentType, fileName);
 */