using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDoApplication.DAL;
using ToDoApplication.Models;

namespace ToDoApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Validate(User u)
        {
            DbAccessLayer db = new DbAccessLayer();
            var check = db.Users.Where(m => m.UserName == u.UserName && m.Password == u.Password).FirstOrDefault();
            if (check == null)
            {
                ViewBag.InvalidLogIn = "Invalid username or password..";
                return View("Index", null);
            }
            else
            {
               FormsAuthentication.SetAuthCookie("Cookie", true); //Set cookie for form authentication 
                Session["UserName"] = u.UserName;
                return RedirectToAction("Enter", "ToDo", null);
            }
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User u)
        {
            DbAccessLayer dal = new DbAccessLayer();
            if(ModelState.IsValid)
            {
                string RePass = Request.Form["RetypePassword"];
                if (RePass == u.Password)
                {
                    dal.Users.Add(u);
                    dal.SaveChanges();
                    TempData["RegistrationSucess"] = "User registered sucessfully..";//tempdata to retain the value
                    return RedirectToAction("Index");
                }
            }
            return View("Register", null);
        }

    }
}