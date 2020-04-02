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
    public class Volunteer
    {
        [Key]
        public int Volunteer_ID { get; set; }
        public string Volunteer_name { get; set; }
        public string Volunteer_description { get; set; }
        public DateTime Volunteer_start_time { get; set; }
        public int Volunteer_maximum_headcount { get; set; }
        public int Volunteer_applied_headcount { get; set; }

        // many2many
        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}
