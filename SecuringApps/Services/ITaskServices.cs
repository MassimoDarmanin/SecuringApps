using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Services
{
    public interface ITaskServices
    {
        TaskModel GetFile(Guid id);

        IQueryable<TaskModel> GetAllFiles();
        //void Add(TaskModel tasks, Guid userId);
        void Add(TaskModel task, string userId);
    }
}
