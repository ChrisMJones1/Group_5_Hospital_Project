using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Group_5_Hospital_Project.Models.VIewModels
{
    public class VolunteersViewModel
    {
        // many 2 many
        public List<Volunteer> Volunteer { get; set; }

        [Display(Name = "Users")]
        public List<ApplicationUserManager> Users { get; set; }
    }
}