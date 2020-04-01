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
    public class Find_Doctor
    {
        [Key]
        public int doctor_id { get; set; }
        public string doctor_name { get; set; }
        public string doctor_email { get; set; }
        public int doctor_phone { get; set; }
        public string doctor_speciality { get; set; }
        public string doctor_availabilty { get; set; }
        public string doctor_experience { get; set; }
    }
}