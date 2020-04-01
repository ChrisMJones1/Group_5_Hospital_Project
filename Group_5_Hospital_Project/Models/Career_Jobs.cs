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
        [Key]
        public int job_id { get; set; }
        public string job_name { get; set; }
        public string job_description { get; set; }
        public string job_requirement { get; set; }
        public string job_type { get; set; }
        public DateTime job_date { get; set; }
    }
}