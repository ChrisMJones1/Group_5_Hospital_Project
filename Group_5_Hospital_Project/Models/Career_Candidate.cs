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
    public class Career_Candidate
    {
        //A candidate is person who applies for jobs posted by the hospital
        //candidate has following properties:
        //    -name
        //    -email
        //    -phone
        //    -type of job they want to apply for..
        //    -address(place where he stays)
        [Key]
        public int candidate_id { get; set; }
        public string candidate_name { get; set; }
        public string candidate_email { get; set; }
        public int candidate_phone { get; set; }
        public string candidate_jobtype { get; set; }
        public string candidate_address { get; set; }


        //here the candidate has  many to many relationship with jobs
        public ICollection<Career_Job> Career_Jobs { get; set; }
    }
}