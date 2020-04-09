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
    public class Send_Best_Wishes {


        /*

        The Send Best Wishes feature will allow people to send thoughtful messages of 
        support to their friends and loved ones who are currently patients in the hospital. 

        Create, Read, Update is done on the public end user side
        Read, Update and Delete is done on the admin side

        Some things that describe an Send Best Wishes Form
        - Form ID
        - Form Title
        - Form Sender Name
        - Form Status
        - Form Message

        */ 
        
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string SenderName { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }



    //Represents the many in (one Patient to many Send Best Wishes Forms)
    public virtual Patient Patient { get; set; }



    }
}