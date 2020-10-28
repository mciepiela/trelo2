using System;
using System.Collections.Generic;
using System.Linq;
using trelo2.Models;
using trelo2.Services.Interfaces;

namespace trelo2.Services
{
    public class TasksServices : ITasksServices
    {
        private readonly ApplicationDbContext _db;
        public TasksServices()
        {
            _db = new ApplicationDbContext();
        }


        public IEnumerable<Task> GetUserTasks(string userId)
        {
            IEnumerable<Task> myTask = _db.Tasks.Include("User").Where(x => x.User.Id.Equals(userId, StringComparison.InvariantCulture));


            return myTask;
        }

        public bool CreateTaskForUser(Task taskToCreate, string userId)
        {
            try
            {
                ApplicationUser currentUser = _db.Users.FirstOrDefault(x => x.Id == userId);

                taskToCreate.User = currentUser;
                taskToCreate.IsReady = false;
                _db.Tasks.Add(taskToCreate);
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}