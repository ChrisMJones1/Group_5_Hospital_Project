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
using System.Diagnostics;

namespace Group_5_Hospital_Project.Controllers
{ //reference:This code is done by reviewing christine's code 
    public class NewsController : Controller
    {
        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();
        // GET: News
        public ActionResult List(string newssearchkey)
        {
          
            Debug.WriteLine("The parameter is " + newssearchkey);

            string query = "Select * from News";

            if (newssearchkey != "")
            {
                query = query + " where news_title like '%" + newssearchkey + "%'";
            }
            //to show all the news
            //var news = db.News.SqlQuery("Select * from News").ToList();
            //By using search news
            List<News> news = db.News.SqlQuery(query).ToList();
           
            return View(news);
        }

        // GET: News/Details/5
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            News news = db.News.SqlQuery("select * from News where news_id=@news_id", new SqlParameter("@news_id", id)).FirstOrDefault();
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //THE [HttpPost] Means that this method will only be activated on a POST form submit to the following URL
        //URL: /News/Add
        [HttpPost]
        public ActionResult Add(string news_title, DateTime news_date, string news_description)
        {

            //Debug.WriteLine("Want to create a news with name " + news_title +" date " + news_date + " and description " + news_description) ;

            string query = "insert into News (news_title,news_date, news_description) values (@news_title,@news_date,@news_description)";
            SqlParameter[] sqlparams = new SqlParameter[3]; //0,1,2 pieces of information to add

            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@news_title", news_title);
            sqlparams[1] = new SqlParameter("@news_date", news_date);
            sqlparams[2] = new SqlParameter("@news_description", news_description);


            //db.Database.ExecuteSqlCommand will run insert, update, delete statements
            //db.News.SqlCommand will run a select statement, for example.
            db.Database.ExecuteSqlCommand(query, sqlparams);


            //run the list method to return to a list of news so we can see our new one!
            return RedirectToAction("List");
        }

        public ActionResult New()
        {

            return View();
        }
        public ActionResult Update(int id)
        {
            //need information about a particular News
            News news = db.News.SqlQuery("select * from News where news_id = @news_id", new SqlParameter("@news_id", id)).FirstOrDefault();
            return View(news);
        }

        [HttpPost]
        public ActionResult Update(int id, string news_title, DateTime news_date, string news_description)
        {


            //Debug.WriteLine("I am trying to edit a news's title to "+news_title+" and news's dateto "+news_date+"change ndescription to "+news_description);

            string query = "update News set news_title=@news_title, news_date=@news_date ,news_description=@news_description  where news_id=@news_id";
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@news_id", id);
            sqlparams[1] = new SqlParameter("@news_title", news_title);
            sqlparams[2] = new SqlParameter("@news_date", news_date);
            sqlparams[3] = new SqlParameter("@news_description", news_description);


            db.Database.ExecuteSqlCommand(query, sqlparams);

            //logic for updating the news in the database goes here
            return RedirectToAction("List");
        }


        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from News where  news_id= @news_id";
            SqlParameter param = new SqlParameter("@news_id", id);
            News news = db.News.SqlQuery(query, param).FirstOrDefault();

            return View(news);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from News where news_id = @news_id";
            SqlParameter param = new SqlParameter("@news_id", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
