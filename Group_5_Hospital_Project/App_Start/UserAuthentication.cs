using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Group_5_Hospital_Project.Models;
using Group_5_Hospital_Project.Data;
using System.Diagnostics;

namespace Group_5_Hospital_Project
{
    //Modified from PetGroomingAuthentication developed by Christine Bittle, for educational purposes only
    public partial class ApplicationUserManager : UserManager<ApplicationUser>
    {

        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();
        private string userid
        {
            get { return HttpContext.Current.User.Identity.GetUserId() != null ? HttpContext.Current.User.Identity.GetUserId() : ""; }
        }
        private bool isLoggedIn
        {
            get { return HttpContext.Current.User.Identity.IsAuthenticated ? true : false; }
        }
        //public property accessors
        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
        }


        private ApplicationUser GetUser()
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            if (userid == "") return null;
            return (manager.FindById(userid));
        }
        //---- this point down the code has to be yours
        public int GetUserPermission()
        {
            ApplicationUser user = GetUser();
            if (user == null) return -5;
            
            return user.Permission;
        }




        public string TestMethod()
        {

            return ("Test Successful");
        }
    }
}