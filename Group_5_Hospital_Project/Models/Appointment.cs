using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group_5_Hospital_Project.Models
{
    public class Appointment
    {
        public int Appointment_ID { get; set; }
        public DateTime Appointment_start_time { get; set; }
        public DateTime Appointment_end_time { get; set; }
        // many2many
        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}