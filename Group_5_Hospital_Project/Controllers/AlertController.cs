using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group_5_Hospital_Project.Data;
using Group_5_Hospital_Project.Models;

namespace Group_5_Hospital_Project.Controllers
{
    public class AlertController : Controller
    {
        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();

        //alert/list
        public ActionResult List()
        {
            List<Alert> alerts = db.Alert.SqlQuery("select * from Alerts").ToList();
            return View(alerts);
        }

        //alert/new
        public ActionResult New()
        {
            return View();
        }

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

        
        public ActionResult Edit(int id)
        {
            string query = "select * from Alerts where alert_id = @id";
            var param = new SqlParameter("@id", id);

            Alert alert = db.Alert.SqlQuery(query, param).FirstOrDefault();

            return View(alert);
        }

        [HttpPost]
        public ActionResult Edit(int id, Alert alert)
        {
            db.Entry(alert).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("List");
        }

        
        public ActionResult Delete(int id)
        {
            string query = "select * from Alerts where alert_id = @id";
            var param = new SqlParameter("@id", id);

            Alert alert = db.Alert.SqlQuery(query, param).FirstOrDefault();

            return View(alert);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Alert alert = db.Alert.Where(a => a.alert_id == id).FirstOrDefault();

            db.Alert.Remove(alert);
            db.SaveChanges();

            return RedirectToAction("List");

        }
    }
}