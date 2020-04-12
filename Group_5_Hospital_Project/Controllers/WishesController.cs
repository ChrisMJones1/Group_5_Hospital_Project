using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group_5_Hospital_Project.Data;
using Group_5_Hospital_Project.Models;
using Group_5_Hospital_Project.Models.ViewModels;
using System.Diagnostics;
using System.IO;

namespace Group_5_Hospital_Project.Controllers
{
    public class WishesController : Controller
    {


        public Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();


        // Go to the add and return the view
        public ActionResult Add()
        {
            return View();
        }



        //add the parameters - form field id's
        [HttpPost]
        public ActionResult New(string wishesname, string sendername, string message)
        {
            //write query to insert
            string query = "INSERT INTO wishes (Title, SenderName, Message) values (@wishesname, @sendername, @message)  ";
            Debug.WriteLine("I am trying to add the" + wishesname);

            SqlParameter[] Sqlparams = new SqlParameter[3]; // number of fields
            Sqlparams[0] = new SqlParameter("@wishesname", wishesname); //first matches the name we give above and the second matches the id from the form
            Sqlparams[1] = new SqlParameter("@senderName", sendername);
            Sqlparams[2] = new SqlParameter("@message", message);

            //execute the query
            db.Database.ExecuteSqlCommand(query, Sqlparams);
            Debug.WriteLine("I am trying to add the" + wishesname);
            //return/redirect to the list
            return RedirectToAction("List");
        }


        public ActionResult New()
        {
            List<Wishes> wishes = db.Wishes.SqlQuery("SELECT * FROM wishes").ToList();

            return View(wishes);
        }

        // GET: SBW
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            //going to the database and getting the SBW
            List<Wishes> wishes = db.Wishes.SqlQuery("SELECT * FROM wishes").ToList();

            return View(wishes);
            //provides a list of SBW
        }


        public ActionResult Show(int id)
        {
            string query = "SELECT * FROM wishes where id = @id";
            var parameter = new SqlParameter("@id", id);
            Wishes selectedwishes = db.Wishes.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedwishes);
        }




        //DELETE 
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from wishes where id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Wishes selectedwishes = db.Wishes.SqlQuery(query, param).FirstOrDefault();
            return View(selectedwishes);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "DELETE FROM wishes where id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);



            return RedirectToAction("List");
        }


    }

}