using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trelo2.Models;
using trelo2.Services;

namespace trelo2.Tests
{
    [TestClass]
    public class TasksServicesTest

    {
        private readonly string _validUserID = "63eb00dc-73ac-4db3-8e64-6971dd3d44d8";
        private readonly string _nullUserID = "";
        private readonly string _notValidUserID = "abc";

        [TestMethod]
        public void GetUserTaskUserIdNull()
        {
            var lol = new TasksServices();
            IEnumerable<Task> listOfMyTasks;
            listOfMyTasks = lol.GetUserTasks(_nullUserID);

            Assert.IsNull(listOfMyTasks);

        }
        [TestMethod]
        public void GetUserTaskCorrectUserId()
        {
            var lol = new TasksServices();
            var listOfMyTasks = lol.GetUserTasks(_validUserID);

            Assert.IsNotNull(listOfMyTasks);

        }
        [TestMethod]
        public void GetUserTaskNotCorrectUserId()
        {
            var lol = new TasksServices();
            var listOfMyTasks = lol.GetUserTasks(_notValidUserID);

            Assert.IsNull(listOfMyTasks);

        }
    }
}
