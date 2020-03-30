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
    public class Feedback_Forms
    {
        [Key]
        public int Feedback_Forms_ID { get; set; }
        public int Feedback_Forms_Rating { get; set; }
        public string Feedback_Forms_Comment { get; set; }
        public string Feedback_Forms_Email { get; set; }
        public DateTime Feedback_Forms_Date { get; set; }
        public ApplicationUser User { get; set; }

    }
}