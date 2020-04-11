using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group_5_Hospital_Project.Models.ViewModels
{
    public class UpdatePatient
    {



        //we need the patient info and a list of forms

        public Patient Patient { get; set; }
        public List<Wishes> Wishes { get; set; }


    }
}