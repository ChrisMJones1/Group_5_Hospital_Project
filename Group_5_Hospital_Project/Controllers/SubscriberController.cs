using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group_5_Hospital_Project.Data;
using Group_5_Hospital_Project.Models;
using PagedList.Mvc;
using PagedList;


namespace Group_5_Hospital_Project.Controllers
{
    public class SubscriberController : Controller
    {

        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();
        // GET: Subscriber
        public ActionResult List(string search, int? i)
        {
            return View(db.Subscriber.Where(s => s.subscriber_email.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 10));

        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Create(Subscriber subscriber)
        {
            db.Subscriber.Add(subscriber);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        //subscriber/show
        public ActionResult Show(int id)
        {
            string query = "select * from Subscribers where subscriber_id = @id";
            var param = new SqlParameter("@id", id);
            Subscriber subscriber = db.Subscriber.SqlQuery(query, param).FirstOrDefault();

            return View(subscriber);
        }

        public ActionResult Edit(int id)
        {
            string query = "select * from Subscribers where subscriber_id = @id";
            var param = new SqlParameter("@id", id);

            Subscriber subscriber = db.Subscriber.SqlQuery(query, param).FirstOrDefault();

            return View(subscriber);
        }

        [HttpPost]
        public ActionResult Edit(int id, Subscriber subscriber)
        {
            db.Entry(subscriber).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            string query = "select * from Subscribers where subscriber_id = @id";
            var param = new SqlParameter("@id", id);

            Subscriber subscriber = db.Subscriber.SqlQuery(query, param).FirstOrDefault();

            return View(subscriber);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Subscriber subscriber = db.Subscriber.Where(s => s.subscriber_id == id).FirstOrDefault();

            db.Subscriber.Remove(subscriber);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}