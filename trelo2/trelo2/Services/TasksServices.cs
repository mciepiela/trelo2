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

            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Brak argumentu. {ex}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Coś poszło nie tak. {ex}");
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
            catch (Exception ArgumentException)

            {
                Console.WriteLine($"argumenException {ArgumentException}");
                throw new ArgumentException(nameof(id));
            }

        }

        public Task EditTaskAjax(int id, bool value)
        {

            Task taskToEdit = _db.Tasks.Find(id); 
            taskToEdit.IsReady = value;
            _db.Entry(taskToEdit).State = EntityState.Modified;
            _db.SaveChanges();
            return taskToEdit;

        }

        public Task EditTaskPost(Task task2)
        {
            try
            {
                _db.Entry(task2).State = EntityState.Modified;
                _db.SaveChanges();
                return task2;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        //GET Edit
        public Task EditTaskGet(int id)
        {
            Task task = _db.Tasks.Find(id);
            if (task != null)
            {
                ApplicationUser currentUser = task.User;
            }

            return task;

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

        public void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}