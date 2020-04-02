using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Group_5_Hospital_Project.Data;

namespace Group_5_Hospital_Project.Models
{
    public class Alert
    {
        [Key]
        public int alert_id { get; set; }
        [Display(Name = "Alert Title")]
        public string alert_title { get; set; }

        [Display(Name = "Alert Body")]
        public string alert_body { get; set; }
        [Display(Name = "Created At")]
        public DateTime created_at { get; set; }

        public Alert()
        {
            created_at = DateTime.Now;
        }
    }
}