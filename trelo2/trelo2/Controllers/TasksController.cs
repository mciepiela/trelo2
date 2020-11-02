using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using trelo2.Models;
using trelo2.Services.Interfaces;

namespace trelo2.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksServices _taskServices;
        private ApplicationDbContext db = new ApplicationDbContext();

        public TasksController(ITasksServices taskServices)
        {
            _taskServices = taskServices;
        }

        // GET: Tasks
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        private IEnumerable<Task> GetMyTasks()
        {
            var currentUserId = User.Identity.GetUserId();

            IEnumerable<Task> myTask = _taskServices.GetUserTasks(currentUserId);
            int amountOfTasks = myTask.Count();

            int readyTaskCount = 0;
            foreach (Task task in myTask)
            {
                if (task.IsReady)
                {
                    readyTaskCount++;
                }
            }

            ViewBag.Percent = Math.Round(100f * ((float)readyTaskCount / (float)amountOfTasks));
            return myTask;
        }

        [Authorize]
        public ActionResult BulidTaskTable()
        {

            return PartialView("_TaskTable", GetMyTasks());

        }

        // GET: Tasks/Details/5
        [Authorize]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Task task = _taskServices.DetailOfTask(id.Value);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        [Authorize]

        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public ActionResult Create([Bind(Include = "Id,Body,IsReady")] Task task)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                task.User = currentUser;
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public ActionResult AjaxCreate([Bind(Include = "Id,Body")] Task task)
        {
            if (!ModelState.IsValid)
            {
                //return valdation error View
            }


            string currentUserId = User.Identity.GetUserId();
            var result = _taskServices.CreateTaskForUser(task, currentUserId);

            if (result == true)
            {
                return PartialView("_TaskTable", GetMyTasks());
            }
            else
            {
                //return
            }

            return PartialView("_ErrorView");


        }

        // GET: Tasks/Edit/5
        [Authorize]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (task.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public ActionResult Edit([Bind(Include = "Id,Body,IsReady")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }
        [HttpPost]
        [Authorize]

        public ActionResult AjaxEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            else
            {
                task = _taskServices.EditTask(id.Value, value);
                return PartialView("_TaskTable", GetMyTasks());
            }


        }
        // GET: Tasks/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)


        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Task task = _taskServices.DeleteTask(id.Value);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]


        public ActionResult DeleteConfirmed(int id)
        {


            Task taskToDelConfirmed = _taskServices.DeleteTaskConfirmed(id);
           
            
            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
