using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;

//https://stackoverflow.com/questions/38846816/how-to-get-custom-property-value-of-the-applicationuser-in-the-asp-net-mvc-5-vie
namespace Group_5_Hospital_Project.Helpers //the following solution was modified from answer by stack exchange user "tmg" for educational purposes only
{
    public static class IdentityExtensions
    {
        public static int Permission(this IIdentity identity)
        {
            return Convert.ToInt32(((ClaimsIdentity)identity).FindFirst("Permission").Value);
        }
    }
}
 
