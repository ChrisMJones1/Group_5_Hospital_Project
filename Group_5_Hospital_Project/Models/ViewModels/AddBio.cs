using Group_5_Hospital_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group_5_Hospital_Project.Models.ViewModels
{
    public class AddBio
    {
        public Staff_Bios staff_bios { get; set; }
        public virtual List<ApplicationUser> staff_list { get; set; }
    }
}