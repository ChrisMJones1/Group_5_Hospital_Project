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
{
    public class Find_DoctorController : Controller
    {
        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();
        // GET: Find_Doctor
       public ActionResult List(string finddoctorsearchkey)
        {
            Debug.WriteLine("The parameter is " + finddoctorsearchkey);

            string query = "Select * from Find_Doctor";

            if (finddoctorsearchkey != "")
            {
                query = query + " where doctor_name like '%" + finddoctorsearchkey + "%'";
            }
            //to show all the doctors
            //var doctor = db.Find_Doctor.SqlQuery("Select * from Find_Doctor").ToList();
            //Byusing serch find a doctor
            List<Find_Doctor> doctor = db.Find_Doctor.SqlQuery(query).ToList();
            return View(doctor);
        }
       

        // GET: Find_Doctor/Details/5
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Find_Doctor doctor = db.Find_Doctor.SqlQuery("select * from Find_Doctor where doctor_id=@doctor_id", new SqlParameter("@doctor_id", id)).FirstOrDefault();

            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }
        //THE [HttpPost] Means that this method will only be activated on a POST form submit to the following URL
        //URL: /Find_Doctor/Add
        [HttpPost]
        public ActionResult Add(string doctor_name, string doctor_email, string doctor_phone, string doctor_specialization)
        {

            //Debug.WriteLine("Want to create a doctor with name " + doctor_name +"email " + doctor_email + " phone " + doctor_phone + " and specialization " + doctor_specialization) ;

            string query = "insert into Find_Doctor (doctor_name,doctor_email, doctor_phone,doctor_specialization) values (@doctor_name,@doctor_email,@doctor_phone,@doctor_specialization)";
            SqlParameter[] sqlparams = new SqlParameter[4]; //0,1,2,3 pieces of information to add

            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@doctor_name", doctor_name);
            sqlparams[1] = new SqlParameter("@doctor_email", doctor_email);
            sqlparams[2] = new SqlParameter("@doctor_phone", doctor_phone);
            sqlparams[3] = new SqlParameter("@doctor_specialization", doctor_specialization);


            //db.Database.ExecuteSqlCommand will run insert, update, delete statements
            //db.Find_Doctor.SqlCommand will run a select statement, for example.
            db.Database.ExecuteSqlCommand(query, sqlparams);


            //run the list method to return to a list of doctors so we can see our new one!
            return RedirectToAction("List");
        }

        public ActionResult New()
        {

            return View();
        }
        //URL: /Find_Doctor/Update
        public ActionResult Update(int id)
        {
            //need information about a particular Doctor
            Find_Doctor doctor = db.Find_Doctor.SqlQuery("select * from Find_Doctor where doctor_id = @doctor_id", new SqlParameter("@doctor_id", id)).FirstOrDefault();
            return View(doctor);
        }

        [HttpPost]
        public ActionResult Update(int id, string doctor_name, string doctor_email, string doctor_phone, string doctor_specialization)
        {


            //Debug.WriteLine("I am trying to edit a doctor's name to "+doctor_name+" and doctor's email to "+doctor_email+" doctor's phone  to "+doctor_phone and+" doctor's specialization  to "+doctor_specialization);

            string query = "update Find_Doctor set doctor_name=@doctor_name, doctor_email=@doctor_email ,doctor_phone=@doctor_phone, doctor_specialization=@doctor_specialization where doctor_id=@doctor_id";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@doctor_id", id);
            sqlparams[1] = new SqlParameter("@doctor_name", doctor_name);
            sqlparams[2] = new SqlParameter("@doctor_email", doctor_email);
            sqlparams[3] = new SqlParameter("@doctor_phone", doctor_phone);
            sqlparams[4] = new SqlParameter("@doctor_specialization", doctor_specialization);


            db.Database.ExecuteSqlCommand(query, sqlparams);

            //logic for updating the doctor in the database goes here
            return RedirectToAction("List");
        }
        //URL: /Find_Doctor/DeleteConfirm
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Find_Doctor where  doctor_id= @doctor_id";
            SqlParameter param = new SqlParameter("@doctor_id", id);
            Find_Doctor doctor= db.Find_Doctor.SqlQuery(query, param).FirstOrDefault();

            return View(doctor);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Find_Doctor where doctor_id = @doctor_id";
            SqlParameter param = new SqlParameter("@doctor_id", id);
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