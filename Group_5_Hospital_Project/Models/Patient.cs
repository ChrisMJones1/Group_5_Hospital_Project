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
    public class Patient
    {

        /*
            A patient is someone who is in the hospital. 

            Things that describe a patient:

            - Patient ID
            - Patient Name
            - Patient Age
            - Patient Diagnosis
            - Patient Room Number
            - Patient Phone Number
            - Patient Email

        */



        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Diagnosis { get; set; }
        public int RoomNumber { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }


        //Represents the "many" in (one Patient to many Send Best Wishes Forms)
        // public ICollection<Wishes> Wishes { get; set; }

        //Represents the many in (one Patient to many Send Best Wishes Forms)        
        public int WishesID { get; set; }
        [ForeignKey("WishesID")]
        public virtual Wishes Wishes { get; set; }

    }
}