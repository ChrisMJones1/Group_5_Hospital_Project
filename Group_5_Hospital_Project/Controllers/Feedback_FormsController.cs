using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group_5_Hospital_Project.Data;
using Group_5_Hospital_Project.Models;
//using Group_5_Hospital_Project.Models.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Group_5_Hospital_Project.Controllers
{
    public class Feedback_FormsController : Controller
    {
        //need this to work with the login functionalities
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        //reference how the Account Controller instantiates the controller class with SignInManager and UserManager

        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();
        //parameterless constructor function
        public Feedback_FormsController() { }
        // GET: Feedback_Forms
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string searchkey, int pagenum = 0)
        {
            string userid = User.Identity.GetUserId();
            int permission = UserManager.GetUserPermission();

            if (permission == 1 || permission == 3) //is patient or admin
            {
                List<Feedback_Forms> feedback_forms;
                //pagination technique modified from LINQ pagination developed by Christine Bittle, for educational purposes only
                if (permission == 1) //is patient
                {
                    feedback_forms = db.Feedback_Forms.Where(g => g.User.Id.Equals(userid)).ToList();

                }
                else // is admin
                {

                    feedback_forms = db.Feedback_Forms.ToList();
                }


                int perpage = 3;
                int formcount = feedback_forms.Count();
                int maxpage = (int)Math.Ceiling((decimal)formcount / perpage) - 1;
                if (maxpage < 0) maxpage = 0;
                if (pagenum < 0) pagenum = 0;
                if (pagenum > maxpage) pagenum = maxpage;
                int start = (int)(perpage * pagenum);
                ViewData["pagenum"] = pagenum;
                ViewData["pagesummary"] = "";
                if (maxpage > 0)
                {
                    ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                    feedback_forms = db.Feedback_Forms
                        .Where(g => (permission == 1) ? g.User.Id.Equals(userid) : true)
                        .OrderBy(g => g.Feedback_Forms_ID)
                        .Skip(start)
                        .Take(perpage)
                        .ToList();
                }
                //end of pagination algorithm (LINQ techniques)

                return View(feedback_forms);

            }
            else
            {
                return Redirect("~");
            }



        }

        public ActionResult Add()
        {
            int permission = UserManager.GetUserPermission();
            if (permission == 1) //is patient
            {
                return View();
            }
            else
            {
                return Redirect("~");
            }
                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Feedback_Forms feedback_form)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            int permission = UserManager.GetUserPermission();

            if (permission == 1 || permission == 3) //is patient or admin
            {
                feedback_form.User = currentUser;
                db.Feedback_Forms.Add(feedback_form);
                db.SaveChanges();
            }
            return RedirectToAction("List", "Feedback_Forms");
        }


        //code modified from Code developed by Christine Bittle, for educational purposes only
        public Feedback_FormsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}