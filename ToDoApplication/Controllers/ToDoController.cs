using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDoApplication.DAL;
using ToDoApplication.Models;
using ToDoApplication.ViewModel;

namespace ToDoApplication.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        // GET: ToDo
        public ActionResult Enter()
        {
            DbAccessLayer db = new DbAccessLayer();
            //select all the users and pass it to the view using viewbag 
            ViewBag.users = db.Users.Select(m => m.UserName).ToList();
            
            TasksVM vm = new TasksVM();
            vm.Tasks = new Task();

            return View("EnterToDo", vm);//first time pass empty object
        }

        public ActionResult Submit()
        {
            DbAccessLayer dal = new DbAccessLayer();//created a DataAccessLayer object
            //TasksVM vm = new TasksVM();//viewModel is no longer required as Jquery post method is used
            Task obj = new Task();//created a model object
            ViewBag.users = dal.Users.Select(m => m.UserName).ToList(); //select all the users and pass it to the view using viewbag 

            obj.TaskCode = Request.Form["Tasks.TaskCode"];
            obj.TaskName = Request.Form["Tasks.TaskName"];
            obj.TaskDescription = Request.Form["Tasks.TaskDescription"];
            // obj.IsComplete = Convert.ToBoolean(Request.Form["Tasks.IsComplete"]);   //not returning true 
            obj.IsComplete = Convert.ToBoolean(Request.Form.GetValues("Tasks.IsComplete")[0]);
            obj.EnteredDate = DateTime.Parse(Request.Form["Tasks.EnteredDate"]);
            obj.AssignedTo = Request.Form["AssignedTo"];

            obj.AssignedBy = "ABC1234";

            if (ModelState.IsValid)
            {
                dal.Tasks.Add(obj); //in-memory updation
                dal.SaveChanges(); //physical commit
                //vm.Tasks = new Task();//no longer required
            }
            //no longer required
            //else
            //{
            //    vm.Tasks = obj;
            //}

            //Now show list of all the created tasks
            DbAccessLayer dl = new DbAccessLayer();
            List<Task> TaskAll = dl.Tasks.ToList<Task>();
            //vm.TaskList = TaskAll;
            //return View("EnterToDo", vm);  //Now we are making jQuery post so no need of vm
            return Json(TaskAll, JsonRequestBehavior.AllowGet);//return json result of task collections
                                           
        
        }


        public ActionResult GetTasks()  //returns Json collection of Task records to jQuery get method
        {
            DbAccessLayer dal = new DbAccessLayer();
            List<Task> TaskAll = dal.Tasks.ToList<Task>();
            Thread.Sleep(2000); // this delay is included just to demostrate the Ajax behaviour
            return Json(TaskAll, JsonRequestBehavior.AllowGet);
        }

        public List<Task> UpdateSelectedTask(Task t)
        {
            //update the selected task n reload the entries
            DbAccessLayer db = new DbAccessLayer();
            Task taskupdate = (from temp in db.Tasks
                               where temp.TaskCode == t.TaskCode
                               select temp).ToList<Task>()[0];
         
            taskupdate.TaskName = Request.Form["Tasks.TaskName"];
            taskupdate.TaskDescription = Request.Form["Tasks.TaskDescription"];
            taskupdate.IsComplete = Convert.ToBoolean(Request.Form.GetValues("Tasks.IsComplete")[0]);
            taskupdate.EnteredDate = DateTime.Parse(Request.Form["Tasks.EnteredDate"]);
            taskupdate.AssignedTo = Request.Form["AssignedTo"];
            //return refreshed list
            List<Task> tasks = db.Tasks.ToList<Task>();
            return tasks;
        }

        public List<Task> DeleteSelectedTas(Task t)
        {
            //delete the selected task n reload the entries
            DbAccessLayer db = new DbAccessLayer();
            Task taskDelete = (from temp in db.Tasks
                               where temp.TaskCode == t.TaskCode
                               select temp).ToList<Task>()[0];
            db.Tasks.Remove(taskDelete);
            db.SaveChanges();
            //return refreshed list
            List<Task> tasks = db.Tasks.ToList<Task>();
            return tasks;

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}