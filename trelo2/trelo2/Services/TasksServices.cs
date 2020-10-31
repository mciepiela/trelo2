﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Ajax.Utilities;
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
                IEnumerable<Task> myTask = _db.Tasks.Include("User").Where(x => x.User.Id.Equals(userId, StringComparison.InvariantCulture));

                return myTask;
            }
                
            catch (Exception userIdIsNull)
            {
                Console.WriteLine(userIdIsNull);
                return null;
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