using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using Group_5_Hospital_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Group_5_Hospital_Project.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    // The application user class is the class that is used to describe someone who is logged in
    // We are leveraging this class by associating it with a Groomer and an Owner.

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //tiny int 1 = guest, 2 = patient, 3 = staff, 4 = admin 
        public int Permission { get; set; }

    }

    //PetGroomingContext class has been adjusted to become a subclass of IdentityDbContext instead of DbContext
    //Why? Because This class helps support base login functionality (IdentityDbContext).
    public class Group_5_Hospital_Project_Context : IdentityDbContext<ApplicationUser>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public Group_5_Hospital_Project_Context() : base("name=Group_5_Hospital_Project_Context")
        {
        }
        public static Group_5_Hospital_Project_Context Create()
        {
            return new Group_5_Hospital_Project_Context();
        }

        //public System.Data.Entity.DbSet<Group_5_Hospital_Project.Models.whateveryourmodel> Pets { get; set; }
        public System.Data.Entity.DbSet<Group_5_Hospital_Project.Models.Staff_Bios> Staff_Bios { get; set; }
        public System.Data.Entity.DbSet<Group_5_Hospital_Project.Models.Feedback_Forms> Feedback_Forms { get; set; }
        public System.Data.Entity.DbSet<Group_5_Hospital_Project.Models.Subscriber> Subscribers { get; set; }
        public System.Data.Entity.DbSet<Group_5_Hospital_Project.Models.Newsletter> Newsletters { get; set; }
        public System.Data.Entity.DbSet<Group_5_Hospital_Project.Models.Alert> Alerts { get; set; }
        public System.Data.Entity.DbSet<Group_5_Hospital_Project.Models.Page> Pages { get; set; }


    }
}