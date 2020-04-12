using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Group_5_Hospital_Project.Data;
using Group_5_Hospital_Project.Models;
using Group_5_Hospital_Project.Models.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Group_5_Hospital_Project.Controllers
{
    public class Staff_BiosController : Controller
    {
        //need this to work with the login functionalities
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        //reference how the Account Controller instantiates the controller class with SignInManager and UserManager

        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();
        //parameterless constructor function
        public Staff_BiosController() { }
        // GET: Staff_Bios
        public ActionResult Index()
        {
            List<Staff_Bios> staff_bios = db.Staff_Bios.ToList();
            return View(staff_bios);
      
        }

        public ActionResult Create()
        {
            int permission = UserManager.GetUserPermission();

            //get list of all staff members without a bio for admin view
            List<ApplicationUser> staff_list = db.Users.Where(s => s.Permission == 2).ToList();
            List<Staff_Bios> staff_bios = db.Staff_Bios.ToList();
            foreach (Staff_Bios _bio in staff_bios)
            {
                if (staff_list.Any(b => b.Id == _bio.User.Id))
                {
                    ApplicationUser _bio_user = db.Users.FirstOrDefault(x => x.Id == _bio.User.Id);
                    staff_list.Remove(_bio_user); //if the staff has a bio, don't include it
                }
            }


            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            int bio_count = db.Staff_Bios.Where(g => g.User.Id.Equals(currentUserId)).Count();

            //create empty staff_bio for the viewmodel
            Staff_Bios bio = new Staff_Bios();

            AddBio add_bio = new AddBio();

            add_bio.staff_bios = bio;

            add_bio.staff_list = staff_list;

            if (permission == 3 || (permission == 2 && bio_count == 0)) //is admin or staff with no bio yet
            {
                return View(add_bio);
            }
            else if((permission == 2 && bio_count > 0)) //if they're staff and have an existing bio, redirect them to the edit page
            {
                //get the ID of the existing bio
                int bio_id = db.Staff_Bios.FirstOrDefault(x => x.User.Id == currentUserId).Staff_Bio_ID; //gets the id of the bio
                return RedirectToAction("Edit", "Staff_Bios", new {@id = bio_id }); //redirects the staff member to the correct edit page
            }
            else
            {
                return Redirect("~");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff_Bios staff_bio, HttpPostedFileBase DocPic, string user_id)
        {
            

            if (ModelState.IsValid)
            {

                string currentUserId = User.Identity.GetUserId();
                int permission = User.Identity.Permission();


                if (permission == 2 || permission == 3) //is staff or admin
                {
                    if(permission == 3)
                    {
                        staff_bio.User = db.Users.FirstOrDefault(x => x.Id == user_id);
                    }
                    else
                    {
                        staff_bio.User = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                    }

                    //file upload code created by Christine Bittle for PetGrooming MVC, modified for educational use only
                    int haspic = 0;
                    string docpicextension = "";
                    if (DocPic != null)
                    {
                        Debug.WriteLine("Something identified...");

                        if (DocPic.ContentLength > 0)
                        {
                            Debug.WriteLine("Successfully Identified Image");
                            //file extensioncheck taken from https://www.c-sharpcorner.com/article/file-upload-extension-validation-in-asp-net-mvc-and-javascript/
                            var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                            var extension = Path.GetExtension(DocPic.FileName).Substring(1);

                            if (valtypes.Contains(extension))
                            {
                                try
                                {
                                    //Using User ID, as every staff member can only have 1 profile and 1 picture
                                    string fn = staff_bio.User.Id + "." + extension;



                                    
                                    string path = Path.Combine(Server.MapPath("~/Content/Bio_Images/"), fn);

                                    //in case an image already exists from possible former creation/deletion of a bio
                                    if (System.IO.File.Exists(path))
                                    {
                                        System.IO.File.Delete(path);
                                    }

                                    DocPic.SaveAs(path);
                                    haspic = 1;
                                    docpicextension = extension;

                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine("Staff Image was not saved successfully.");
                                    Debug.WriteLine("Exception:" + ex);
                                }



                            }
                        }
                    }

                    if(haspic == 1)
                    {
                        staff_bio.Staff_Bio_Image_Path = staff_bio.User.Id + "." + docpicextension;
                    }
                    db.Staff_Bios.Add(staff_bio);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Staff_Bios");
            }
            return View();

        }


        public ActionResult Edit(int id)
        {
            string userid = User.Identity.GetUserId();
            int permission = UserManager.GetUserPermission();
            bool edit_permission = true;
            if (permission == 2 || permission == 3) //is staff or admin
            {
                Staff_Bios staff_bio;
                //pagination technique modified from LINQ pagination developed by Christine Bittle, for educational purposes only
                if (permission == 1) //is patient
                {
                    staff_bio = db.Staff_Bios.Find(id);
                    edit_permission = (staff_bio.User.Id == userid);

                }
                else // is admin
                {

                    staff_bio = db.Staff_Bios.Find(id);
                }



                if (staff_bio != null && edit_permission == true) //check that we have a result and they have permission
                {
                    return View(staff_bio);
                }

                else
                {
                    return Redirect("~");
                }
            }
            else
            {
                return Redirect("~");
            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff_Bios staff_bio, HttpPostedFileBase DocPic)
        {


            if (ModelState.IsValid)
            {


                int permission = User.Identity.Permission();


                if (permission == 2 || permission == 3) //is staff or admin
                {
                    Staff_Bios updated_bio = db.Staff_Bios.Find(staff_bio.Staff_Bio_ID);
                    //file upload code created by Christine Bittle for PetGrooming MVC, modified for educational use only
                    int haspic = 0;
                    string docpicextension = "";
                    if (DocPic != null)
                    {
                        Debug.WriteLine("Something identified...");

                        if (DocPic.ContentLength > 0)
                        {
                            Debug.WriteLine("Successfully Identified Image");
                            //file extensioncheck taken from https://www.c-sharpcorner.com/article/file-upload-extension-validation-in-asp-net-mvc-and-javascript/
                            var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                            var extension = Path.GetExtension(DocPic.FileName).Substring(1);

                            if (valtypes.Contains(extension))
                            {
                                try
                                {
                                    //Using User ID, as every staff member can only have 1 profile and 1 picture
                                    string fn = updated_bio.User.Id + "." + extension;


                                    string path = Path.Combine(Server.MapPath("~/Content/Bio_Images/"), fn);


                                    //to overwrite the previous image
                                    if (System.IO.File.Exists(path))
                                    {
                                        System.IO.File.Delete(path);
                                    }


                                    DocPic.SaveAs(path);
                                    haspic = 1;
                                    docpicextension = extension;

                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine("Staff Image was not saved successfully.");
                                    Debug.WriteLine("Exception:" + ex);
                                }



                            }
                        }
                    }

                    if (haspic == 1)
                    {
                        updated_bio.Staff_Bio_Image_Path = updated_bio.User.Id + "." + docpicextension;
                    }
                    updated_bio.Staff_Bio_Name = staff_bio.Staff_Bio_Name;
                    updated_bio.Staff_Bio_Text = staff_bio.Staff_Bio_Text;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Staff_Bios");
            }
            return View();

        }

        public ActionResult Details(int id)
        {
            Staff_Bios staff_bio = db.Staff_Bios.Find(id);
            if (staff_bio != null) //if there is an actual entry returned
            {
                return View(staff_bio);
            }
            return RedirectToAction("Index", "Staff_Bios"); //otherwise reroute them to the main list
        }

        //create return for delete page when confirming details
        public ActionResult Delete(int id)
        {
            Staff_Bios staff_bio = db.Staff_Bios.Find(id);
            if (staff_bio != null && //if there is an actual entry returned
                (
                (User.Identity.Permission() == 2 && staff_bio.User.Id == User.Identity.GetUserId()) //and they are the staff who created the bio
                || User.Identity.Permission() == 3 //or an admin
                )
                )
            {
                return View(staff_bio);
            }
            return RedirectToAction("List", "Staff_Bios"); //otherwise reroute them to the main list
        }

        //create method for actually deleting entry when confirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string confirm)
        {

            string userid = User.Identity.GetUserId();
            int permission = UserManager.GetUserPermission();

            Staff_Bios delete_entry = db.Staff_Bios.Find(id);
            if (delete_entry != null)
            {
                if ((permission == 2 && delete_entry.User.Id == userid) || permission == 3) //is staff who created bio or admin
                {
                    //if the request is from the staff who created the bio or an admin, delete the entry
                    //Also, delete the image (if it exists) from the database

                    if(delete_entry.Staff_Bio_Image_Path != null)
                    {
                        try
                        {
                            string path = Path.Combine(Server.MapPath("~/Content/Bio_Images/"), delete_entry.Staff_Bio_Image_Path);


                            //Delete Picture if it exists
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Staff Image was not deleted successfully.");
                            Debug.WriteLine("Exception:" + ex);
                        }
                    }

                    db.Staff_Bios.Remove(delete_entry);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Staff_Bios");
            }


            return View();
        }

        //code modified from Code developed by Christine Bittle, for educational purposes only
        public Staff_BiosController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}