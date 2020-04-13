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
            if (permission == 1 || permission == 3) //is patient or admin
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
            if (ModelState.IsValid)
            {


                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

                int permission = UserManager.GetUserPermission();

                if (permission == 1 || permission == 3) //is patient or admin
                {
                    feedback_form.User = currentUser;
                    feedback_form.Feedback_Forms_Email = currentUser.Email;
                    db.Feedback_Forms.Add(feedback_form);
                    db.SaveChanges();
                }
                return RedirectToAction("List", "Feedback_Forms");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            string userid = User.Identity.GetUserId();
            int permission = UserManager.GetUserPermission();
            bool edit_permission = true;
            if (permission == 1 || permission == 3) //is patient or admin
            {
                Feedback_Forms feedback_form;
                //pagination technique modified from LINQ pagination developed by Christine Bittle, for educational purposes only
                if (permission == 1) //is patient
                {
                    feedback_form = db.Feedback_Forms.Find(id);
                    edit_permission = (feedback_form.User.Id == userid);

                }
                else // is admin
                {

                    feedback_form = db.Feedback_Forms.Find(id);
                }



                if (feedback_form != null && edit_permission == true) //check that we have a result and they have permission
                {
                    return View(feedback_form);
                }

                else
                {
                    return Redirect("~");
                }
            }
            else
            {
                return Redirect("~");
            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Feedback_Forms feedback_form)
        {
            if (ModelState.IsValid)
            {


                string userid = User.Identity.GetUserId();
                int permission = UserManager.GetUserPermission();

                Feedback_Forms updated_form = db.Feedback_Forms.Find(feedback_form.Feedback_Forms_ID);
                if (updated_form != null)
                {
                    if ((permission == 1 && updated_form.User.Id == userid) || permission == 3) //is patient who submitted the form or admin
                    {
                        updated_form.Feedback_Forms_Comment = feedback_form.Feedback_Forms_Comment;
                        updated_form.Feedback_Forms_Date = feedback_form.Feedback_Forms_Date;
                        updated_form.Feedback_Forms_Rating = feedback_form.Feedback_Forms_Rating;
                        db.SaveChanges();
                    }
                    return RedirectToAction("List", "Feedback_Forms");
                }

            }
            return View();
        }

        //create details return for admin and the patients who submitted it

        public ActionResult Details(int id)
        {
            Feedback_Forms feedback_form = db.Feedback_Forms.Find(id);
            if (feedback_form != null && //if there is an actual entry returned
                (
                (User.Identity.Permission() == 1 && feedback_form.User.Id == User.Identity.GetUserId()) //and they are the patient who submitted the form 
                || User.Identity.Permission() == 3 //or an admin
                )
                )
            {
                return View(feedback_form);
            }
            return RedirectToAction("List", "Feedback_Forms"); //otherwise reroute them to the main list
        }

        //create return for delete page when confirming details
        public ActionResult Delete(int id)
        {
            Feedback_Forms feedback_form = db.Feedback_Forms.Find(id);
            if (feedback_form != null && //if there is an actual entry returned
                (
                (User.Identity.Permission() == 1 && feedback_form.User.Id == User.Identity.GetUserId()) //and they are the patient who submitted the form 
                || User.Identity.Permission() == 3 //or an admin
                )
                )
            {
                return View(feedback_form);
            }
            return RedirectToAction("List", "Feedback_Forms"); //otherwise reroute them to the main list
        }

        //create method for actually deleting entry when confirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string confirm)
        {



            string userid = User.Identity.GetUserId();
            int permission = UserManager.GetUserPermission();

            Feedback_Forms delete_entry = db.Feedback_Forms.Find(id);
            if (delete_entry != null)
            {
                if ((permission == 1 && delete_entry.User.Id == userid) || permission == 3) //is patient who submitted the form or admin
                {
                    //if the request is from the patient who submitted the form or an admin, delete the entry
                    db.Feedback_Forms.Remove(delete_entry);
                    db.SaveChanges();
                }
                return RedirectToAction("List", "Feedback_Forms");
            }


            return View();
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