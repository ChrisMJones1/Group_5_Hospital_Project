using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Group_5_Hospital_Project.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Group_5_Hospital_Project.Models.VIewModels
{
    public class VolunteersViewModel
    {
        // many 2 many
        // why is C# so hard......??? or why am i so stupid?
        public List<Volunteer> Volunteer { get; set; }

        public ApplicationUser Users { get; set; }
    }
}