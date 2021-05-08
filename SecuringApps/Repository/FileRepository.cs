using SecuringApps.Data;
using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Repository
{
    public class FileRepository : IFileRepository
    {
        SecuringAppDbContext context;

        //private List<File> _fileList;

        public FileRepository(SecuringAppDbContext _context)
        {
            context = _context;
        }

        public Guid Add(File files)
        {
            context.Files.Add(files);
            context.SaveChanges();
            return files.Id;
        }

        public IQueryable<File> GetAllFiles()
        {
            return context.Files;
        }

        public File GetFile(Guid id)
        {
            //Single or default will return one product or null
            return context.Files.SingleOrDefault(x => x.Id == id);
        }
    }
}
