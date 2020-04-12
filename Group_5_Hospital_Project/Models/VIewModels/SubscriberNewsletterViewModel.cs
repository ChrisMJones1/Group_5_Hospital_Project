using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Group_5_Hospital_Project.Models;

namespace Group_5_Hospital_Project.Models.VIewModels
{
    public class SubscriberNewsletterViewModel
    {
        public Newsletter newsletter { get; set; }

        [Display(Name = "Subscribers")]
        public List<Subscriber> subscribers { get; set; }
    }
}