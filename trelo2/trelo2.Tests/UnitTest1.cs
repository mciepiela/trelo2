using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trelo2.Models;
using trelo2.Services;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace trelo2.Tests
{



    public class TasksServicesTestXunit

    {
        private readonly string _validUserID = "63eb00dc-73ac-4db3-8e64-6971dd3d44d8";
        private readonly string _nullUserID = null;
        private readonly string _notValidUserID = "abc";



        [Fact]
        public void GetUserTaskCorrectUserId()
        {
            TasksServices lol = new TasksServices();
            IEnumerable<Task> listOfMyTasks = lol.GetUserTasks(_validUserID);

            Assert.IsNotNull(listOfMyTasks);

        }

        [Fact]
        public void GetingUserTaskUserIdIsNull_ShouldReturnException()
        {
            TasksServices lol = new TasksServices();
            IEnumerable<Task> listOfUserTask = lol.GetUserTasks(_nullUserID);

        }
    }
}

