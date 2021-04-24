using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SecuringApps.Data;
using SecuringApps.Models;
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

        public FileController(SecuringAppDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }

        public IActionResult Index(Guid id)
        {
            IEnumerable<FileModel> fileList = _db.Files.Where(f => f.TaskId == id);
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
        public async Task<IActionResult> CreateAsync(FileModel files)
        {
            if (ModelState.IsValid)
            {
                //save image to wwwroot/attachment
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(files.Attachment.FileName);
                string extention = Path.GetExtension(files.Attachment.FileName);

                files.FileName = fileName = fileName + DateTime.Now.ToString("yymmdd");
                string path = Path.Combine(wwwRootPath + "/Attachment/", fileName) + extention;

                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                   await files.Attachment.CopyToAsync(fileStream);
                }

                //insert record
                _db.Files.Add(files);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(files);
        }
    }
}
