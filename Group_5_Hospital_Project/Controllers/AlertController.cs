using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group_5_Hospital_Project.Data;
using Group_5_Hospital_Project.Models;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList.Mvc;
using PagedList;


namespace Group_5_Hospital_Project.Controllers
{
    public class AlertController : Controller
    {

        //to be able to work with login functionality
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();

        public AlertController() { }

        //alert/list
        public ActionResult List(string search, int? i)
        {
            return View(db.Alert.Where(a => a.alert_title.Contains(search) || a.alert_body.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 10));
        }

        //alert/new
        [Authorize]
        public ActionResult New()
        {
            if (User.Identity.Permission() == 3 || User.Identity.Permission() ==2) { }
            return View();
        }

        [Authorize]
        public ActionResult Create(Alert alert)
        {
            db.Alert.Add(alert);
            db.SaveChanges();

            return RedirectToAction("List", "Alert");
        }

        //Alert/show
        public ActionResult Show(int id)
        {
            string query = "select * from Alerts where alert_id = @id";

            var param = new SqlParameter("@id", id);
            Alert alert = db.Alert.SqlQuery(query, param).FirstOrDefault();

            return View(alert);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            string query = "select * from Alerts where alert_id = @id";
            var param = new SqlParameter("@id", id);

            Alert alert = db.Alert.SqlQuery(query, param).FirstOrDefault();

            return View(alert);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, Alert alert)
        {
            db.Entry(alert).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("List");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            string query = "select * from Alerts where alert_id = @id";
            var param = new SqlParameter("@id", id);

            Alert alert = db.Alert.SqlQuery(query, param).FirstOrDefault();

            return View(alert);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Alert alert = db.Alert.Where(a => a.alert_id == id).FirstOrDefault();

            db.Alert.Remove(alert);
            db.SaveChanges();

            return RedirectToAction("List");

        }
    }
}