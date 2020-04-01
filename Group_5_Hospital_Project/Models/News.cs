using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group_5_Hospital_Project.Models
{
    public class News
    {
        [Key]
        public int news_id { get; set; }
        public string news_title { get; set; }
        public DateTime newsposted_date { get; set; }
        public DateTime newsupdated_date { get; set; }
        public string news_information{ get; set; }
       
    }

}