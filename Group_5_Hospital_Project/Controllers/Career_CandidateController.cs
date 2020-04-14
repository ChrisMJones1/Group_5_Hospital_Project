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
    public class Career_CandidateController : Controller
    {
        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();
        // GET: Career_Candidate
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string candidatesearchkey, int pagenum=0)
        {
            Debug.WriteLine("The parameter is " + candidatesearchkey);
            //query to select from the list of candidates
            string query = "Select * from Career_Candidate";
            if (candidatesearchkey != "")
            {
                query = query + " where candidate_name like '%" + candidatesearchkey + "%'";
            }

            //list of candidates
            List<Career_Candidate> career_Candidates = db.Career_Candidates.SqlQuery(query).ToList();
            //returning the candidates list

            //code for pagination
            //here we define number of enteries per page
            //then check if it exceeds limit
            //enteries should be moved to next page
            int perpage = 3;
            int candidatecount = career_Candidates.Count();
            int maxpage = (int)Math.Ceiling((decimal)candidatecount / perpage) - 1;
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

                if (candidatesearchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + candidatesearchkey + "%"));
                    ViewData["candidatesearchkey"] = candidatesearchkey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " candidate by candidate_id offset @start rows fetch first @perpage rows only ";
                Debug.WriteLine(pagedquery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + perpage);
                //end of the pagination code
                career_Candidates = db.Career_Candidates.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }

            return View(career_Candidates);
        }
        //to push the data for the new entry of the candidates
        public ActionResult New()
        {

            return View();
        }

        //to pull the data from the fields and send it to the database
        [HttpPost]
        public ActionResult Add(string candidate_name, string candidate_email,int candidate_phone, string candidate_jobtype, string candidate_address)
        {
            string query = "insert into Career_Candidate (candidate_name, candidate_email, candidate_phone, candidate_jobtype,candidate_address) values (@candidate_name, @candidate_email, @candidate_phone, @candidate_jobtype,@candidate_address)";

            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@candidate_name", candidate_name);
            sqlparams[1] = new SqlParameter("@candidate_email", candidate_email);
            sqlparams[2] = new SqlParameter("@candidate_phone", candidate_phone);
            sqlparams[3] = new SqlParameter("@candidate_jobtype", candidate_jobtype);
            sqlparams[4] = new SqlParameter("@candidate_address", candidate_address);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");//sending the data for addition to database
        }

        // functionality to show the candidates along with option to add and remove job for that particular candidate who is applying for jobs.
        public ActionResult Show(int id)
        {
            //here we are selecting one particular candidate for particular id.
            string main_query = "select * from Career_Candidate where candidate_id = @id";
            var pk_parameter = new SqlParameter("@id", id);
            Career_Candidate career_Candidate = db.Career_Candidates.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            //here we are getting data from job table where the job id is referred by the candidate table using bridging table.
            string aside_query = "select * from Career_Job inner join Career_JobCareer_Candidate on Career_Job.job_id = Career_JobCareer_Candidate.Career_Job_job_id where Career_JobCareer_Candidate.Career_Candidate_candidate_id=@id";
            var fk_parameter = new SqlParameter("@id", id);
            List<Career_Job> jobs = db.Career_Jobs.SqlQuery(aside_query, fk_parameter).ToList();
            // here we filling the drop down to add more job which arenot yet applied by candidate
            string all_Career_Jobs_query = "select * from Career_Job";
            List<Career_Job> Alljobs = db.Career_Jobs.SqlQuery(all_Career_Jobs_query).ToList();

            //using viewmodel for showing candidates and all the available jobs.
            showCandidates viewmodel = new showCandidates();
            viewmodel.Career_Candidate = career_Candidate;
            viewmodel.Career_Jobs = jobs;
            viewmodel.all_Career_Jobs = Alljobs;

            return View(viewmodel);
        }


        // functionality to add a job which is not yet applied by the candidate
        [HttpPost]
        public ActionResult Attachjob(int id, int job_id)
        {
            Debug.WriteLine("candidate_id is" + id + " and job_id is " + job_id);


            string check_query = "select * from Career_Job inner join Career_JobCareer_Candidate on Career_JobCareer_Candidate.Career_Job_job_id = Career_Job.job_id where Career_Job_job_id=@job_id and Career_Candidate_candidate_id=@id";
            SqlParameter[] check_params = new SqlParameter[2];
            check_params[0] = new SqlParameter("@id", id);
            check_params[1] = new SqlParameter("@job_id",job_id);
            List<Career_Job> career_Jobs = db.Career_Jobs.SqlQuery(check_query, check_params).ToList();

            if (career_Jobs.Count <= 0)
            {
                // accessing the bridging table for candidates and job
                string query = "insert into Career_JobCareer_Candidate (Career_Job_job_id, Career_Candidate_candidate_id) values (@job_id, @id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@job_id", job_id);


                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            return RedirectToAction("Show/" + id);

        }
        //functionality to remove a job from the bridging table not from the job table
        [HttpGet]
        public ActionResult Detachjob(int id, int job_id)
        {

            Debug.WriteLine("candidate_id is" + id + " and job_id is " + job_id);

            string query = "delete from Career_JobCareer_Candidate where Career_Job_job_id=@job_id and Career_Candidate_candidate_id=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@job_id", job_id);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("Show/" + id);
        }
        // here we are trying to delete the candidate
        // to confirm if they want to delete 
        // here only a particular `candidate is selected .
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Career_Candidate where candidate_id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Career_Candidate career_Candidate = db.Career_Candidates.SqlQuery(query, param).FirstOrDefault();
            return View(career_Candidate);
        }
        // here the selected candidate is deleted form the database and database is updated .
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Career_Candidate where candidate_id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }
        //to update and make changes the details of the candidate
        // here the data for a particular id is selected and displayed
        public ActionResult Update(int id)
        {
            string query = "select * from Career_Candidate where candidate_id=@id";
            var parameter = new SqlParameter("@id", id);
            Career_Candidate career_Candidate = db.Career_Candidates.SqlQuery(query, parameter).FirstOrDefault();

            return View(career_Candidate);
        }
        // Here the updated data is pull from the fields and then updated in the database
        [HttpPost]
        public ActionResult Update(int id, string candidate_name, string candidate_email,int candidate_phone, string candidate_jobtype, string candidate_address)
        {
            //database query
            string query = "update Career_Candidate set candidate_name=@candidate_name, candidate_email=@candidate_email, candidate_phone=@candidate_phone, candidate_jobtype=@candidate_jobtype,candidate_address=@candidate_address where candidate_id = @id";
            //parameters used to access the fields
            SqlParameter[] sqlparams = new SqlParameter[6];

            sqlparams[0] = new SqlParameter("@candidate_name", candidate_name);
            sqlparams[1] = new SqlParameter("@candidate_email", candidate_email);
            sqlparams[2] = new SqlParameter("@candidate_phone", candidate_phone);
            sqlparams[3] = new SqlParameter("@candidate_jobtype", candidate_jobtype);
            sqlparams[4] = new SqlParameter("@candidate_address", candidate_address);
            sqlparams[5] = new SqlParameter("@id", id);


            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");//returning the updated list.
        }

    }
}