using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group_5_Hospital_Project.Models.ViewModels
{
    public class showCandidates
    {
        //one individual candidate
        public virtual Career_Candidate Career_Candidate { get; set; }
        //list of jobs for the candidates who applied
        public List<Career_Job> Career_Jobs { get; set; }


        //a SEPARATE list for representing the ADD of an candidate to job,
        //a dropdownlist of all jobs that a candidate can apply for.
        public List<Career_Job> all_Career_Jobs { get; set; }
    }
}