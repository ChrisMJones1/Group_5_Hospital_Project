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
    public class Career_JobController : Controller
    {
        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();
        // GET: Job
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string jobsearchkey, int pagenum=0)
        {

            Debug.WriteLine("The search key is " + jobsearchkey);
            //query to select from the list of jobs
            string query = "Select * from Career_Job";

            if (jobsearchkey != "")
            {

                query = query + " where job_name like '%" + jobsearchkey + "%'";
                Debug.WriteLine("The query is " + query);
            }
            // to get the list
            List<Career_Job> career_Jobs = db.Career_Jobs.SqlQuery(query).ToList();


            int perpage = 3;
            int jobcount = career_Jobs.Count();
            int maxpage = (int)Math.Ceiling((decimal)jobcount / perpage) - 1;
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

                if (jobsearchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + jobsearchkey + "%"));
                    ViewData["jobsearchkey"] = jobsearchkey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by job_id offset @start rows fetch first @perpage rows only ";
                Debug.WriteLine(pagedquery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + perpage);
                career_Jobs = db.Career_Jobs.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }


            return View(career_Jobs);

        }
        //here all the data entered is taken into account and then submitted
        [HttpPost]
        public ActionResult Add(string job_name, string job_requirement, string job_description, string job_type, DateTime job_date)
        {

            string query = "insert into Career_Job (job_name, job_requirement, job_description, job_type, job_date) values (@job_name,@job_requirement,@job_description,@job_type,@job_date)";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@job_name", job_name);
            sqlparams[1] = new SqlParameter("@job_requirement", job_requirement);
            sqlparams[2] = new SqlParameter("@job_description", job_description);
            sqlparams[3] = new SqlParameter("@job_type", job_type);
            sqlparams[4] = new SqlParameter("@job_date", job_date);


            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }
        //to push the data to the database 
        public ActionResult Add()
        {

            return View();
        }
        public ActionResult Show(int? id)
        {
            //if there is nothing to show
            if (id == null)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Career_Job career_Job = db.Career_Jobs.SqlQuery("select * from Career_Job where job_id=@job_id", new SqlParameter("@job_id", id)).FirstOrDefault();
            if (career_Job == null)
            {
                return HttpNotFound();
            }
            //database query to select data from jobs and candidates table .
            string query = "select * from Career_Candidate inner join Career_JobCareer_Candidate on Career_Candidate.candidate_id= Career_JobCareer_Candidate. Career_Candidate_candidate_id where Career_Job_job_id = @id";
            SqlParameter param = new SqlParameter("@id", id);
            List<Career_Candidate> candidates_job = db.Career_Candidates.SqlQuery(query, param).ToList();

            //using view model for showing the jobs where the candidates are displayed for one job.
            showCareer_Job viewmodel = new showCareer_Job();
            viewmodel.Career_Jobs = career_Job;
            viewmodel.Career_Candidates = candidates_job;


            return View(viewmodel);
        }
        // function to update i.e to make changes in the enteries of the jobs details
        public ActionResult Update(int id)
        {
            string query = "select * from Career_Job where job_id = @id";
            var parameter = new SqlParameter("@id", id);
            Career_Job selectedjob = db.Career_Jobs.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedjob);
        }
        //here we are updating the information and also updating them in database.
        [HttpPost]
        public ActionResult Update(int id, string job_name, string job_type,string job_requirement, string job_description, DateTime job_date)
        {
            string query = "update Career_Job set job_name= @job_name,job_type=@job_type,job_requirement=@job_requirement,job_description=@job_description,job_date=@job_date where job_id = @id";
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@job_name", job_name);
            sqlparams[1] = new SqlParameter("@job_type", job_type);
            sqlparams[2] = new SqlParameter("@job_requirement", job_requirement);
            sqlparams[3] = new SqlParameter("@job_description", job_description);
            sqlparams[4] = new SqlParameter("@job_date", job_date);
            sqlparams[5] = new SqlParameter("@id", id);


            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
        //here it is asking for confirmation from the user if they want to delete this particular id
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Career_Job where job_id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Career_Job selectedjob = db.Career_Jobs.SqlQuery(query, param).FirstOrDefault();
            return View(selectedjob);
        }

        // here the job will be deleted if this function is called
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Career_Job where job_id =@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("list");
        }
    }
}