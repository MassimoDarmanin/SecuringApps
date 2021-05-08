using SecuringApps.Models;
using SecuringApps.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Services
{
    public class FileServices : IFileServices
    {
        private IFileRepository fileRepo;

        public FileServices(IFileRepository fileRepository)
        {
            fileRepo = fileRepository;
        }

        public FileModel GetFile(Guid id)
        {
            var myFile = fileRepo.GetFile(id);
            FileModel model = new FileModel();

            model.FileName = myFile.FileName;
            model.DateUploaded = myFile.DateUploaded;
            //model.FileType = myFile.FileType;
            model.Extension = myFile.Extension;
            model.UserId = myFile.UserId;
            model.Attachment = myFile.Attachment;
            //model.TaskId = myFile.TaskId;
            model.UserEmail = myFile.UserEmail;
            //model.Signature = myFile.Signature;

            /*model.Tasks = new TaskModel()
            {
                Id = model.Tasks.Id,
                Title = model.Tasks.Title
            };*/
            return model;
        }
        

        public void Add(FileModel file, string userId, string userEmail, string fileName, string path, string taskId)
        {
            Guid obj = Guid.NewGuid();

            //converting to productviewmodel to product
            File newFile = new File()
            {
                Id = obj,
                FileName = fileName,
                DateUploaded = DateTime.Now,
                FileType = file.FileType,
                Extension = path,
                //Attachment = file.Attachment,
                //TaskId = file.Tasks.Id,
                TaskId = taskId,
                UserId = userId,
                UserEmail = userEmail,
                Signature = file.Signature
            };

            fileRepo.Add(newFile);
        }

        public IQueryable<FileModel> GetAllFiles()
        {
            var list = from f in fileRepo.GetAllFiles()
                       select new FileModel()
                       {
                           Id = f.Id,
                           FileName = f.FileName,
                           DateUploaded = f.DateUploaded,
                           FileType = f.FileType,
                           Extension = f.Extension,
                           TaskId = f.TaskId,
                           UserId = f.UserId,
                           UserEmail = f.UserEmail,
                           Signature = f.Signature
                       };
            return list;
        }
    }
}
