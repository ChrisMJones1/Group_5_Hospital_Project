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
    public class Slideshow
    {

        /*

        A Slideshow is a compact and interactive way to show information on the 
        homepage of the website. Allowed file types in .JPE and .JPEG 
        format only with a file size of 2MB or less are allowed.

        It is made up of single images that rotate through on an interval.

        Some things that describe an image
        - Image ID
        - Image Title
        - Image Description
        - Image Alt Text
        - Image URL

        */

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AltText { get; set; }
        public string ImageURL { get; set; }


    }
}