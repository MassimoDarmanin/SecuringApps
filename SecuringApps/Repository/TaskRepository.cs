using SecuringApps.Data;
using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Repository
{
    public class TaskRepository : ITaskRepository
    {
        SecuringAppDbContext context;

        public TaskRepository(SecuringAppDbContext _context)
        {
            context = _context;
        }

        public Guid AddTask(Tasked Task)
        {
            context.Taskeds.Add(Task);
            context.SaveChanges();
            return Task.Id;
        }

        public IQueryable<Tasked> GetAllTasks()
        {
            return context.Taskeds;
        }

        public Tasked GetTask(Guid Id)
        {
            return context.Taskeds.SingleOrDefault(x => x.Id == Id);
        }
    }
}
