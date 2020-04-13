using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group_5_Hospital_Project.Data;
using Group_5_Hospital_Project.Models;
using Group_5_Hospital_Project.Properties;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Group_5_Hospital_Project.Controllers
{
    public class VolunteersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();

        // GET: Volunteers
        public ActionResult Index()
        {
            // 0-Guest,1-patient,2-staff,3-admin
            int permission = UserManager.GetUserPermission();
            if (permission == Settings.Default.USERTYPE_PATIENT || permission == Settings.Default.USERTYPE_STAFF || permission == Settings.Default.USERTYPE_ADMIN)
            {
                // have access
                return View(db.Volunteers.ToList());
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
