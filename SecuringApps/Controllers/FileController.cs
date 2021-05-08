using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        //private readonly SecuringAppDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<FileController> _logger;
        private readonly IFileServices _fileServices;

        public FileController(IFileServices fileServices, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager, ILogger<FileController> logger)
        {
            //_db = db;
            this._hostEnvironment = hostEnvironment;
            _userManager = userManager;
            _logger = logger;
            _fileServices = fileServices;
        }

        public string Message { get; set; }

        /*public IActionResult Index(Guid id)
        {
            IEnumerable<FileModel> fileList = _db.Files.Where(f => f.TaskId == id);
            return View(fileList);
        }*/

        //[SampleActionFilter]
        public IActionResult Index(string id)
        {
            string userName = _userManager.GetUserName(User);            

            Message = "User: " + userName + $"\nFile Index visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);

            string idDecrypted = Encryption.SymmetricDecrypt(id);
            //Guid guid = new Guid(idDecrypted);

            //var list = _fileServices.GetFile(guid);
            var list = _fileServices.GetAllFiles();
            var listIndex = list.Where(f => f.TaskId == idDecrypted);
            //IQueryable<FileModel> fileList = _db.Files.Where(f => f.TaskId == guid);
            //return View(fileList);
            return View(listIndex);
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
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(FileModel files)
        {
            string userId = _userManager.GetUserId(User);
            //string tId = TaskId;

            //string idDecrypted = Encryption.SymmetricDecrypt(id);
            //Guid guid = new Guid(idDecrypted);

            try
            {
                if (ModelState.IsValid)
                {
                    //save image to wwwroot/attachment
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    //string fileName = Path.GetFileNameWithoutExtension(files.Attachment.FileName);
                    //string extention = Path.GetExtension(files.Attachment.FileName);

                    //files.FileName = fileName = fileName + DateTime.Now.ToString("yymmdd");
                    //string path = Path.Combine(wwwRootPath + "/Attachment/", fileName) + extention;

                    //using(var fileStream = new FileStream(path, FileMode.Create))
                   // {
                        //await files.Attachment.CopyToAsync(fileStream);
                   // }

                    //insert record
                    //files.UserId = Guid.Parse(userId);
                    //files.TaskId = Guid.Parse(tId);
                    //_db.Files.Add(files);
                    
                    //await _db.SaveChangesAsync();

                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Task");
                }
                return View(files);
            }
            catch(Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = "Error while saving File." });
            }

            
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FileModel files, IFormFile fileParam)
        {
            string userId = _userManager.GetUserId(User);
            Dictionary<string, List<byte[]>> whiteList = new Dictionary<string, List<byte[]>>();

            whiteList.Add(".pdf", new List<byte[]>());
            whiteList[".pdf"].Add(new byte[] { 37, 80, 68, 70, 45});

            try
            {
                /*if(fileParam == null)
                {
                    ModelState.AddModelError("fileParam", "Please do not leave empty.");
                    return View("Error", new ErrorViewModel() { Message = "Please do not leave empty." });
                }*/

                if(fileParam.ContentType != "application/pdf")
                {
                    ModelState.AddModelError("fileParam", "Upload only pdf.");
                    return View("Error", new ErrorViewModel() { Message = "Upload only pdf." });
                }

                if(!whiteList.ContainsKey(Path.GetExtension(fileParam.FileName)))
                //if (Path.GetExtension(fileParam.FileName) != ".jpg")
                {
                    ModelState.AddModelError("fileParam", "File submission not valid.");
                    return View("Error", new ErrorViewModel() { Message = "File submission not valid." });
                }

                using (var stream = fileParam.OpenReadStream())
                {
                    stream.Position = 0;

                    long fileLen = stream.Length;

                    if (fileLen < 4 || fileLen > 10485760) //10mb
                    {
                        ModelState.AddModelError("fileParam","File cannot exceed 10mb.");
                        return View("Error", new ErrorViewModel() { Message = "File cannot exceed 10mb." });
                    }

                    byte[] buffer = new byte[5];

                    stream.Read(buffer, 0, buffer.Length);

                    List<byte[]> whiteListBuffers = whiteList[Path.GetExtension(fileParam.FileName)];

                    foreach (byte[] whiteListBuffer in whiteListBuffers)
                    {
                        //if filestream matches whitelist file is safe
                        //if it never matches then it is NOT safe
                    }

                    /*
                    //Read first 5 bytes of file
                    //File Signature

                    //convert from hex to dec

                    //HEX 25 = DEC 37
                    int byte1 = stream.ReadByte();

                    //HEX 50 = DEC 80
                    int byte2 = stream.ReadByte();

                    //HEX 44 = DEC 68
                    int byte3 = stream.ReadByte();

                    //HEX 46 = DEC 70
                    int byte4 = stream.ReadByte();

                    //HEX 2D = DEC 45
                    int byte5 = stream.ReadByte();*/

                    if(buffer[0] == 37 && buffer[1] == 80 && buffer[2] == 68 && buffer[3] == 70 && buffer[4] == 45)
                    {
                        //pattern mathces to value = safe
                    }
                    else
                    {
                        ModelState.AddModelError("fileParam", "File not valid.");
                        return View("Error", new ErrorViewModel() { Message = "File not valid." });
                    }

                    stream.Position = 0;

                    //unique name
                    string fileName = Guid.NewGuid() + Path.GetExtension(fileParam.FileName);

                    string path = _hostEnvironment.WebRootPath + @"\Attachment\" + fileName;
                    using (FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
                    {
                        stream.CopyTo(fs);
                    }
                }

                return RedirectToAction("Index", "Task");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = "Error while saving File." });
            }
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