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
    public class Donation
    {
        //A donor is a person who gives charity for the purpose of serving the patients and community for their health care
        //    the properties that define a donation are:
        //    -name
        //    -email
        //    -phone
        //    -country
        //    -address
        //    -date of donation

        [Key]
        public int donor_id { get; set; }
        public string donor_name { get; set; }
        public string donor_email { get; set; }
        public int donor_phone { get; set; }
        public string donor_country { get; set; }
        public string donor_address { get; set; }
        public DateTime donor_date { get; set; }
    }
}