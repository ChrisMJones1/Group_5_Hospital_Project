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
    public class PageController : Controller
    {
        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();

        // GET: Page
        //Page/List
        public ActionResult List()
        {
            List<Page> pages = db.Page.SqlQuery("select * from Pages").ToList();
            return View(pages);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Create(Page page)
        {
            db.Page.Add(page);
            db.SaveChanges();

            return RedirectToAction("List", "Page");
        }

        public ActionResult Show(int id)
        {
            string query = "select * from Pages where id = @id";
            var param = new SqlParameter("@id", id);
            Page page = db.Page.SqlQuery(query, param).FirstOrDefault();
            return View(page);
        }

        public ActionResult Edit(int id)
        {
            string query = "select * from Pages where id = @id";
            var param = new SqlParameter("@id", id);

            Page page = db.Page.SqlQuery(query, param).FirstOrDefault();

            return View(page);
        }

        [HttpPost]
        public ActionResult Edit(int id, Page page)
        {
            db.Entry(page).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            string query = "select * from Pages where id = @id";
            var param = new SqlParameter("@id", id);

            Page page = db.Page.SqlQuery(query, param).FirstOrDefault();

            return View(page);

        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Page page = db.Page.Where(p => p.id == id).FirstOrDefault();

            db.Page.Remove(page);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}