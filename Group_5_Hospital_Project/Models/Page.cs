using System;
using System.ComponentModel.DataAnnotations;

namespace Group_5_Hospital_Project.Models
{
    public class Page
    {
        [Key]
        public int id { get; set; }
        public string content_title { get; set; }
        public string content_body { get; set; }
        public DateTime DateTime { get; set; }
    }
}