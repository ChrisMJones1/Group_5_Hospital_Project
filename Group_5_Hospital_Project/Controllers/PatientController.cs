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
using Group_5_Hospital_Project.Models.ViewModels;
using System.Diagnostics;
using System.IO;


namespace Group_5_Hospital_Project.Controllers
{
    public class PatientController : Controller
    {


        public Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();


        // Go to the add and return the view
        public ActionResult Add()
        {

            return View();
        }

        //add the parameters - form field id's
        [HttpPost]
        public ActionResult New(string patientname, int patientage, string patientdia, int patientroom, int patientphone, string patientemail, int WishesID)
        {
            //write query to insert 
            string query = "INSERT INTO patients (Name, Age, Diagnosis, RoomNumber, PhoneNumber, Email, WishesID) values (@patientname, @patientage, @patientdia, @patientroom, @patientphone, @patientemail, @WishesID)  ";
            Debug.WriteLine("I am trying to add the" + patientname);

            SqlParameter[] Sqlparams = new SqlParameter[7]; // number of fields
            Sqlparams[0] = new SqlParameter("@patientname", patientname); //first matches the name we give above and the second matches the id from the form
            Sqlparams[1] = new SqlParameter("@patientage", patientage);
            Sqlparams[2] = new SqlParameter("@patientdia", patientdia);
            Sqlparams[3] = new SqlParameter("@patientroom", patientroom);
            Sqlparams[4] = new SqlParameter("@patientphone", patientphone);
            Sqlparams[5] = new SqlParameter("@patientemail", patientemail);
            Sqlparams[6] = new SqlParameter("@wishesID", WishesID);


            //execute the query
            db.Database.ExecuteSqlCommand(query, Sqlparams);
            Debug.WriteLine("I am trying to add the" + patientname);
            //return/redirect to the list
            return RedirectToAction("List");

        }


        public ActionResult New()
        {

            List<Wishes> wishes = db.Wishes.SqlQuery("SELECT * FROM Wishes").ToList();

            return View(wishes);
        }


        // GET: Patient

        public ActionResult List()
        {

            List<Patient> patient = db.Patients.SqlQuery("SELECT * FROM Patients").ToList();
            return View(patient);

        }


        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Patient Patient = db.Patients.SqlQuery("select * from patients WHERE id=@id", new SqlParameter("@id", id)).FirstOrDefault();
            if (Patient == null)
            {
                return HttpNotFound();
            }

            ShowPatient viewmodel = new ShowPatient();
            viewmodel.patient = Patient;

            return View(viewmodel);
        }

        public ActionResult Update(int id)
        {
            //info about the patient
            Patient selectedpatient = db.Patients.SqlQuery("select * from patients WHERE id = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<Wishes> wishes = db.Wishes.SqlQuery("SELECT * FROM Wishes").ToList();


            UpdatePatient UpdatePatientViewModel = new UpdatePatient();
            UpdatePatientViewModel.Patient = selectedpatient;
            UpdatePatientViewModel.Wishes = wishes;

            return View(UpdatePatientViewModel);
        }



        [HttpPost]


        public ActionResult Update(int id, string patientname, int patientage, string patientdia, int patientroom, int patientphone, string patientemail, int WishesID)
        {

            string query = "update patients set Name=@patientname, Wishes=@WishesID, Age=@patientage, Diagnosis=@patientdia, RoomNumber=@patientroom, PhoneNumber=@patientphone, Email=@patientemail,  WHERE id=@id";
            SqlParameter[] Sqlparams = new SqlParameter[8];

            Sqlparams[0] = new SqlParameter("@patientname", patientname); //first matches the name we give above and the second matches the id from the form
            Sqlparams[1] = new SqlParameter("@patientage", patientage);
            Sqlparams[2] = new SqlParameter("@patientdia", patientdia);
            Sqlparams[3] = new SqlParameter("@patientroom", patientroom);
            Sqlparams[4] = new SqlParameter("@patientphone", patientphone);
            Sqlparams[5] = new SqlParameter("@patientemail", patientemail);
            Sqlparams[6] = new SqlParameter("@id", id);
            Sqlparams[7] = new SqlParameter("@WishesID", id);



            db.Database.ExecuteSqlCommand(query, Sqlparams);

            //says to update the patient in the database here
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "SELECT * FROM patients WHERE id = @id";
            SqlParameter param = new SqlParameter("@id", id);
            Patient selectedpatient = db.Patients.SqlQuery(query, param).FirstOrDefault();

            return View(selectedpatient);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "DELETE FROM patients WHERE id = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }




    }

}