using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Repository
{
    public interface ITaskRepository
    {
        Tasked GetTask(Guid Id);

        IQueryable<Tasked> GetAllTasks();

        Guid AddTask(Tasked Task);
    }
}
