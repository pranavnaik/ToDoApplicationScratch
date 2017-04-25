using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoApplication.Models;

namespace ToDoApplication.ViewModel
{
    public class TasksVM
    {
        //This ViewModel is created to perform Task entering and displaying on same view 
        public Task Tasks { get; set; }
        public List<Task> TaskList { get; set; }

    }
}