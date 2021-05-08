using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Repository
{
    public interface IFileRepository
    {
        File GetFile(Guid Id);

        //IEnumerable<File> GetAllFiles();
        IQueryable<File> GetAllFiles();

        Guid Add(File files);
    }
}
