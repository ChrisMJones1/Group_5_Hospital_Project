using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group_5_Hospital_Project.Data;
using Group_5_Hospital_Project.Models;
using Group_5_Hospital_Project.Models.VIewModels;
using Group_5_Hospital_Project.Properties;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

namespace Group_5_Hospital_Project.Controllers
{
    public class VolunteersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();

        // GET: Volunteers
        public ActionResult Index(int? page)
        {
            // 0-Guest,1-patient,2-staff,3-admin
            int permission = UserManager.GetUserPermission();
            if (permission == Settings.Default.USERTYPE_PATIENT || permission == Settings.Default.USERTYPE_STAFF || permission == Settings.Default.USERTYPE_ADMIN)
            {
                // have access
                IPagedList<Volunteer> query = db.Volunteers.ToList().ToPagedList(page ?? 1, 3);
                return View(query);
            }
            else
            {
                // (permission == Settings.Default.USERTYPE_GUEST)
                // to login page
                return Redirect("Account/Login");
            }
        }

        // GET: Volunteers/Details/5
        public ActionResult Details(int? id)
        {
            int permission = UserManager.GetUserPermission();
            if (permission == Settings.Default.USERTYPE_PATIENT || permission == Settings.Default.USERTYPE_STAFF || permission == Settings.Default.USERTYPE_ADMIN)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Volunteer volunteer = db.Volunteers.Find(id);
                if (volunteer == null)
                {
                    return HttpNotFound();
                }
                return View(volunteer);
            }
            else
            {
                return Redirect("/Volunteers");
            }
        }

        // GET: Volunteers/Create
        public ActionResult Create()
        {
            // staff and admins can create a volunteer posting
            int permission = UserManager.GetUserPermission();
            if (permission == Settings.Default.USERTYPE_STAFF || permission == Settings.Default.USERTYPE_ADMIN)
            {
                // have access
                return View();
            }
            else
            {
                // to Volunteers page
                // todo: message tells users no access
                return Redirect("/Volunteers");
            }
        }

        // POST: Volunteers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Volunteer_ID,Volunteer_name,Volunteer_description,Volunteer_start_time,Volunteer_maximum_headcount,Volunteer_applied_headcount")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Volunteers.Add(volunteer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volunteer);
        }

        // GET: Volunteers/Edit/5
        public ActionResult Edit(int? id)
        {
            // staff and admins can create a volunteer posting
            int permission = UserManager.GetUserPermission();
            if (permission == Settings.Default.USERTYPE_STAFF || permission == Settings.Default.USERTYPE_ADMIN)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Volunteer volunteer = db.Volunteers.Find(id);
                if (volunteer == null)
                {
                    return HttpNotFound();
                }
                // have access
                return View(volunteer);
            }
            else
            {
                // to Volunteers page
                // todo: message tells users no access
                return Redirect("/Volunteers");
            }
        }

        // POST: Volunteers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Volunteer_ID,Volunteer_name,Volunteer_description,Volunteer_start_time,Volunteer_maximum_headcount,Volunteer_applied_headcount")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteer);
        }

        // GET: Volunteers/Delete/5
        public ActionResult Delete(int? id)
        {

            // staff and admins can create a volunteer posting
            int permission = UserManager.GetUserPermission();
            if (permission == Settings.Default.USERTYPE_STAFF || permission == Settings.Default.USERTYPE_ADMIN)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Volunteer volunteer = db.Volunteers.Find(id);
                if (volunteer == null)
                {
                    return HttpNotFound();
                }
                return View(volunteer);
            }
            else
            {
                // to Volunteers page
                // todo: message tells users no access
                return Redirect("/Volunteers");
            }
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Volunteer volunteer = db.Volunteers.Find(id);
            db.Volunteers.Remove(volunteer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Volunteers/Apply/5
        public ActionResult Apply(int? id)
        {
            // todo : viewmodel
            // users(patient) can apply for a volunteer job
            int permission = UserManager.GetUserPermission();
            if (permission == Settings.Default.USERTYPE_PATIENT)
            {
                // there should be an id
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // current user id
                string currentUserId = User.Identity.GetUserId();
                // get current user
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

                // select from volunteers table and users table?
                string query = "insert into VolunteerApplicationUsers " +
                    "(Volunteer_Volunteer_ID, ApplicationUser_Id) " +
                    "values (@Volunteer_Volunteer_ID, @ApplicationUser_Id)";

                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@Volunteer_Volunteer_ID", id);
                sqlparams[1] = new SqlParameter("@ApplicationUser_Id", currentUserId);

                // execute
                try
                {
                    db.Database.ExecuteSqlCommand(query, sqlparams);
                }
                catch (Exception ex)
                {
                    // let's assume that the exception is "duplicate"
                    // can't really alert here without going to /Apply/id page. i am dying.
                    Response.Write("<script>alert('you have already applied for this volunteer position!!')</script>");
                    //Response.Redirect("/Volunteers");
                    return RedirectToAction("/");

                    throw ex;
                }

                //var Volunteer = db.Volunteers.SqlQuery(query).ToList();
                //var viewModel = new VolunteersViewModel()
                //{
                //    Users = currentUser,
                //    Volunteer = Volunteer
                //};

                // added
                //Response.Write("<script>alert('applied')</script>");
                //Response.End();
                return RedirectToAction("/");
            }
            else
            {
                // to Volunteers page
                // todo: message tells users no access
                return Redirect("/");
            }
        }

        // POST: Volunteers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply([Bind(Include = "Volunteer_ID,Volunteer_name,Volunteer_description,Volunteer_start_time,Volunteer_maximum_headcount,Volunteer_applied_headcount")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteer);
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
