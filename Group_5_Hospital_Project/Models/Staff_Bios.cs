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
    public class Staff_Bios
    {
        [Key]
        public int Staff_Bio_ID { get; set; }
        public string Staff_Bio_Name { get; set; }
        public string Staff_Bio_Text { get; set; }
        public string Staff_Bio_Image_Path { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}