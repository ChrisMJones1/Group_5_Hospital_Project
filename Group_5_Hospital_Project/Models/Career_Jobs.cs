using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Group_5_Hospital_Project.Data;


namespace Group_5_Hospital_Project.Models
{
    public class Career_Job
    {
        //A job is profession or mode of work for which a candidate apply for in order to provide their service
        //    properties defining a job are:
        //    -name
        //    -description of job
        //    -requirement
        //    -type of job
        //    -date of the posting a particular job
        [Key]
        public int job_id { get; set; }
        public string job_name { get; set; }
        public string job_description { get; set; }
        public string job_requirement { get; set; }
        public string job_type { get; set; }
        public DateTime job_date { get; set; }

        //here the job has  many to many relationship with candidates
        public ICollection<Career_Candidate> Career_Candidates { get; set; }
    }
}