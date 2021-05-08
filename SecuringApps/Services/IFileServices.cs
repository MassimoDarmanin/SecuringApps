using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Services
{
    public interface IFileServices
    {
        //void GetFile(FileModel file);
        FileModel GetFile(Guid id);

        IQueryable<FileModel> GetAllFiles();
        //IQueryable<FileModel> GetAllFiles();

        void Add(FileModel files);
    }
}
