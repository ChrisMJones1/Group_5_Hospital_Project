using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group_5_Hospital_Project.Models.ViewModels
{
    public class showCareer_Job
    {
        //here we are trying to get values of the career job table along with candidates who applied or want to apply for job

        public virtual Career_Job Career_Jobs { get; set; }

        //list  of many candidates who have applied for the job
        public List<Career_Candidate> Career_Candidates { get; set; }
    }
}