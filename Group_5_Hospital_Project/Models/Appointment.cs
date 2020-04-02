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
    public class Appointment
    {
        [Key]
        public int Appointment_ID { get; set; }
        public DateTime Appointment_start_time { get; set; }
        public DateTime Appointment_end_time { get; set; }
        // many2many
        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}