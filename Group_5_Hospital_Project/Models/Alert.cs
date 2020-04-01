using System;
using System.ComponentModel.DataAnnotations;

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