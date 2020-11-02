using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
            //if (userId == null)

            try
            {
                IEnumerable<Task> myTask = _db.Tasks.Include("User")
                    .Where(x => x.User.Id.Equals(userId, StringComparison.InvariantCulture));

                return myTask;
            }

            catch (Exception userIdIsNull)
            {
                Console.WriteLine(userIdIsNull);
                return null;
            }



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

        public Task DetailOfTask(int id)
        {
            // if id == null
            try
            {
                Task task = _db.Tasks.Find(id);

                return task;
            }
            catch (Exception argumentException)
            {
                Console.WriteLine(argumentException);
                throw;
            }

        }

        public Task EditTask(int id, bool value)
        {




            Task taskToEdit = _db.Tasks.Find(id); 
                taskToEdit.IsReady = value;
                    _db.Entry(taskToEdit).State = EntityState.Modified;
                    _db.SaveChanges();
                    return taskToEdit;
           
                



        }

        public Task DeleteTask(int id)
        {
            Task taskToDel = _db.Tasks.Find(id);
            _db.Tasks.Remove(taskToDel);

            return taskToDel;

        }
        public Task DeleteTaskConfirmed(int id)
        {
            
            try
            {
                var taskToDelConfirmed = _db.Tasks.Find(id);
                _db.Tasks.Remove(taskToDelConfirmed);
               _db.SaveChanges();

                return taskToDelConfirmed;
            }
            catch (Exception argumenteException)
            {
                Console.WriteLine(argumenteException);
                throw;
            }
            

        }
    }
}