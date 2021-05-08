using SecuringApps.Models;
using SecuringApps.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Services
{
    public class TaskService : ITaskServices
    {
        private ITaskRepository taskRepo;

        public TaskService(ITaskRepository taskRepository)
        {
            taskRepo = taskRepository;
        }


        public void Add(TaskModel tasks, string userId)
        {
            Guid obj = Guid.NewGuid();
           
            //converting to productviewmodel to product
            Tasked newTask = new Tasked()
            {
                Id = obj,
                Title = tasks.Title,
                Description = tasks.Description,
                Deadline = tasks.Deadline,
                UserId = Guid.Parse(userId)
            };

            taskRepo.AddTask(newTask);
        }

        public IQueryable<TaskModel> GetAllFiles()
        {
            var list = from t in taskRepo.GetAllTasks()
                       select new TaskModel()
                       {
                           Id = t.Id,
                           Title = t.Title,
                           Deadline = t.Deadline,
                           Description = t.Description,
                           UserId = t.UserId
                       };
            return list;
        }

        public TaskModel GetFile(Guid id)
        {
            var myTask = taskRepo.GetTask(id);
            TaskModel model = new TaskModel();

            model.Title = myTask.Title;
            model.Description = myTask.Description;
            model.Deadline = myTask.Deadline;
            model.UserId = myTask.UserId;

            return model;
        }
    }
}
