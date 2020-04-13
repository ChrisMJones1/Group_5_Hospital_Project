using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Group_5_Hospital_Project.Data;
using PagedList.Mvc;
using PagedList;
using System.Diagnostics;
using Group_5_Hospital_Project.Models.VIewModels;


namespace Group_5_Hospital_Project.Models
{
    public class NewsletterController : Controller
    {
        private Group_5_Hospital_Project_Context db = new Group_5_Hospital_Project_Context();

        //newsletter/List
        public ActionResult List(string search, int? i)
        {
            return View(db.Newsletter.Where(n => n.newsletter_title.Contains(search) || n.newsletter_body.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 10));
        }

        //adding subscribers to newsletter form of subscribers id =?

        public ActionResult NewsletterSubscriber(int id)
        {
            string query = "select * from Newsletter inner join SubscriberNewsletter on " +
                "Newsletter.newsletter_id = SubscriberNewsletter.Newsletter_newsletter_id " +
                "where Subscriber_subscriber_id = @id";

            SqlParameter param = new SqlParameter("@id", id);
            List<Subscriber> NewsletterSubscribers = db.Subscriber.SqlQuery(query, param).ToList();

            SubscriberNewsletterViewModel viewModel = new SubscriberNewsletterViewModel();
            viewModel.subscribers = NewsletterSubscribers;

            return View(viewModel);
        }


        //newsletter/new
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Create(Newsletter newsletter)
        {
            db.Newsletter.Add(newsletter);
            db.SaveChanges();

            return RedirectToAction("List", "Newsletter");
        }

        //newsletter/show
        public ActionResult Show(int id)
        {
            string query = "select * from Newsletters where newsletter_id = @id";
            var param = new SqlParameter("@id", id);
            Newsletter newsletter = db.Newsletter.SqlQuery(query, param).FirstOrDefault();
            return View(newsletter);
        }

        public ActionResult Edit(int id)
        {
            string query = "select * from Newsletters where newsletter_id = @id";
            var param = new SqlParameter("@id", id);

            Newsletter newsletter = db.Newsletter.SqlQuery(query, param).FirstOrDefault();

            return View(newsletter);
        }

        [HttpPost]
        public ActionResult Edit(int id, Newsletter newsletter)
        {
            db.Entry(newsletter).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("List");
        }

        

        public ActionResult Delete(int id)
        {
            string query = "select * from Newsletters where newsletter_id = @id";
            var param = new SqlParameter("@id", id);

            Newsletter newsletter = db.Newsletter.SqlQuery(query, param).FirstOrDefault();

            return View(newsletter);

        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Newsletter newsletter = db.Newsletter.Where(n => n.newsletter_id == id).FirstOrDefault();

            db.Newsletter.Remove(newsletter);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        // GET: Newsletter
        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("hospitalhumber@gmail.com", "Hospital");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "group5hospital";

                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }

                    return View();

                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Error Sending email";
            }

            return View();
        }
    }
}