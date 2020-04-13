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
    public class Subscriber
    {
        [Key]
        public int subscriber_id { get; set; }
        [Display(Name = "Subscriber Email")]
        public string subscriber_email { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Newsletter> Newsletters { get; set; }
    }
}