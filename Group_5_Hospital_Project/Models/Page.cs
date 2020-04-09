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
    public class Page
    {
        [Key]
        public int id { get; set; }

        [Display(Name="Content Title")]
        public string content_title { get; set; }

        [Display(Name = "Content Body")]
        public string content_body { get; set; }
        public DateTime DateTime { get; set; }

        public Page()
        {
            DateTime = DateTime.Now;
        }
    }
}