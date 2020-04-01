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
        [Key]
        public int candidate_id { get; set; }
        public string candidate_name { get; set; }
        public string candidate_email { get; set; }
        public int candidate_phone { get; set; }
        public string candidate_jobtype { get; set; }
        public string candidate_address { get; set; }
    }
}