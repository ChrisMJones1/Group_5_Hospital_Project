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
    public class Alert
    {
        [Key]
        public int alert_id { get; set; }
        public string alert_title { get; set; }
        public string alert_body { get; set; }
        public DateTime created_at { get; set; }
    }
}