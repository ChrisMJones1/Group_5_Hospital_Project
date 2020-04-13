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
using System.IO;
namespace Group_5_Hospital_Project.Models
{
    public class DonationController : Controller
    {
        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();
        public ActionResult Index()
        {
            return View();
        }
        // to provide the list of the donations to list link and also to add the search functionality for the donations
        public ActionResult list(string donationsearchkey, int pagenum = 0)
        {
            Debug.WriteLine("The parameter is " + donationsearchkey);

            string query = "Select * from Donations";
            if (donationsearchkey != "")
            {
                query = query + " where donor_name like '%" + donationsearchkey + "%'";
            }

            //we need a donation list to search from 
            List<Donation> Donations = db.Donations.SqlQuery(query).ToList();

            int perpage = 3;
            int donationcount = Donations.Count();
            int maxpage = (int)Math.Ceiling((decimal)donationcount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";
            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                List<SqlParameter> newparams = new List<SqlParameter>();

                if (donationsearchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + donationsearchkey + "%"));
                    ViewData["donationsearchkey"] = donationsearchkey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by donor_id offset @start rows fetch first @perpage rows only ";
                Debug.WriteLine(pagedquery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + perpage);
                Donations = db.Donations.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            return View(Donations);
        }
        // to add a new donation
        // this to pull the data from the divs
        public ActionResult add()
        {

            return View();
        }

        //to pull the data from the fields and send it to the database
        [HttpPost]
        public ActionResult add(string donor_name, string donor_email, int donor_phone, string donor_country, string donor_address, DateTime donor_date)
        {
            string query = "insert into Donations (donor_name,donor_email,donor_phone,donor_country,donor_address,donor_date) values (@donor_name,@donor_email,@donor_phone,@donor_country,@donor_address,@donor_date)";

            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@donor_name", donor_name);
            sqlparams[1] = new SqlParameter("@donor_email", donor_email);
            sqlparams[2] = new SqlParameter("@donor_phone", donor_phone);
            sqlparams[3] = new SqlParameter("@donor_country", donor_country);
            sqlparams[4] = new SqlParameter("@donor_address", donor_address);
            sqlparams[5] = new SqlParameter("@donor_date", donor_date);


            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("list");//sending the data for addition to database
        }

        //to update the donation
        // here we are getting information about the particular donation
        public ActionResult Update(int id)
        {
            string query = "select * from Donations where donor_id = @id";
            var parameter = new SqlParameter("@id", id);
            Donation selectedDonations = db.Donations.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedDonations);
        }
        //here we are updating the information and also updating them in database.
        [HttpPost]
        public ActionResult Update(int id, string donor_name, string donor_email, int donor_phone, string donor_country, string donor_address, DateTime donor_date)
        {
            string query = "update Donations set donor_name= @donor_name,donor_email=@donor_email,donor_phone=@donor_phone,donor_country=@donor_country,donor_address=@donor_address,donor_date=@donor_date where donor_id = @id";
            SqlParameter[] sqlparams = new SqlParameter[7];
            sqlparams[0] = new SqlParameter("@donor_name", donor_name);
            sqlparams[1] = new SqlParameter("@donor_email", donor_email);
            sqlparams[2] = new SqlParameter("@donor_phone", donor_phone);
            sqlparams[3] = new SqlParameter("@donor_country", donor_country);
            sqlparams[4] = new SqlParameter("@donor_address", donor_address);
            sqlparams[5] = new SqlParameter("@donor_date", donor_date);
            sqlparams[6] = new SqlParameter("@id", id);


            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("list");
        }
        // to show the deatils of donation
        public ActionResult show(int id)
        {
            string query = "select * from Donations where donor_id = @id";// database query
            var parameter = new SqlParameter("@id", id);
            Donation selectedDonations = db.Donations.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedDonations);
        }
        // here we are trying to delete the donation
        // to confirm if they want to delete 
        // here only a particular donation is selected .
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Donations where donor_id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Donation selectedDonations = db.Donations.SqlQuery(query, param).FirstOrDefault();
            return View(selectedDonations);
        }
       
        // here the donation will be deleted if this function is called
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Donations where donor_id =@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("list");
        }
    }
}