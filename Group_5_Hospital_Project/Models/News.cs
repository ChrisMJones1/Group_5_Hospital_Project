using System;
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
    public class News
    {
        [Key]
        
        public int news_id { get; set; }
        public string news_title { get; set; }
        public DateTime news_date { get; set; }
        public string news_description { get; set; }

    }

}