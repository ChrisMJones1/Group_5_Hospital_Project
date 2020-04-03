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
    public class Newsletter
    {
        [Key]
        public int newsletter_id { get; set; }
        [Display(Name = "Subject")]
        public string newsletter_title { get; set; }
        [Display(Name = "Body")]
        public string newsletter_body { get; set; }
        public ICollection<Subscriber> Subscribers { get; set; }
    }
}