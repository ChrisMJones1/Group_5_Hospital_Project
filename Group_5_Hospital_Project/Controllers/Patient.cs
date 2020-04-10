using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group_5_Hospital_Project.Data;
using Group_5_Hospital_Project.Models;
using System.Diagnostics;
using System.IO;

namespace Group_5_Hospital_Project.Controllers
{
    public class Patient : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
    }
}