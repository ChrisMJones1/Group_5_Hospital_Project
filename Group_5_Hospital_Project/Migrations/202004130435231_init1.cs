namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        alert_id = c.Int(nullable: false, identity: true),
                        alert_title = c.String(),
                        alert_body = c.String(),
                        created_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.alert_id);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Appointment_ID = c.Int(nullable: false, identity: true),
                        Appointment_start_time = c.DateTime(nullable: false),
                        Appointment_end_time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Appointment_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Permission = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        Volunteer_ID = c.Int(nullable: false, identity: true),
                        Volunteer_name = c.String(),
                        Volunteer_description = c.String(),
                        Volunteer_start_time = c.DateTime(nullable: false),
                        Volunteer_maximum_headcount = c.Int(nullable: false),
                        Volunteer_applied_headcount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Volunteer_ID);
            
            CreateTable(
                "dbo.Career_Candidate",
                c => new
                    {
                        candidate_id = c.Int(nullable: false, identity: true),
                        candidate_name = c.String(),
                        candidate_email = c.String(),
                        candidate_phone = c.Int(nullable: false),
                        candidate_jobtype = c.String(),
                        candidate_address = c.String(),
                    })
                .PrimaryKey(t => t.candidate_id);
            
            CreateTable(
                "dbo.Career_Job",
                c => new
                    {
                        job_id = c.Int(nullable: false, identity: true),
                        job_name = c.String(),
                        job_description = c.String(),
                        job_requirement = c.String(),
                        job_type = c.String(),
                        job_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.job_id);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        donor_id = c.Int(nullable: false, identity: true),
                        donor_name = c.String(),
                        donor_email = c.String(),
                        donor_phone = c.Int(nullable: false),
                        donor_country = c.String(),
                        donor_address = c.String(),
                        donor_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.donor_id);
            
            CreateTable(
                "dbo.Feedback_Forms",
                c => new
                    {
                        Feedback_Forms_ID = c.Int(nullable: false, identity: true),
                        Feedback_Forms_Rating = c.Int(nullable: false),
                        Feedback_Forms_Comment = c.String(),
                        Feedback_Forms_Email = c.String(),
                        Feedback_Forms_Date = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Feedback_Forms_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Find_Doctor",
                c => new
                    {
                        doctor_id = c.Int(nullable: false, identity: true),
                        doctor_name = c.String(),
                        doctor_email = c.String(),
                        doctor_phone = c.Int(nullable: false),
                        doctor_specialization = c.String(),
                    })
                .PrimaryKey(t => t.doctor_id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        news_id = c.Int(nullable: false, identity: true),
                        news_title = c.String(),
                        news_date = c.DateTime(nullable: false),
                        news_description = c.String(),
                    })
                .PrimaryKey(t => t.news_id);
            
            CreateTable(
                "dbo.Newsletters",
                c => new
                    {
                        newsletter_id = c.Int(nullable: false, identity: true),
                        newsletter_title = c.String(),
                        newsletter_body = c.String(),
                    })
                .PrimaryKey(t => t.newsletter_id);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        subscriber_id = c.Int(nullable: false, identity: true),
                        subscriber_email = c.String(),
                    })
                .PrimaryKey(t => t.subscriber_id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        content_title = c.String(),
                        content_body = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Diagnosis = c.String(),
                        RoomNumber = c.Int(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(),
                        WishesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wishes", t => t.WishesID, cascadeDelete: true)
                .Index(t => t.WishesID);
            
            CreateTable(
                "dbo.Wishes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SenderName = c.String(),
                        Status = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Slideshows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        AltText = c.String(),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Staff_Bios",
                c => new
                    {
                        Staff_Bio_ID = c.Int(nullable: false, identity: true),
                        Staff_Bio_Name = c.String(),
                        Staff_Bio_Text = c.String(),
                        Staff_Bio_Image_Path = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Staff_Bio_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ApplicationUserAppointments",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Appointment_Appointment_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Appointment_Appointment_ID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Appointments", t => t.Appointment_Appointment_ID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Appointment_Appointment_ID);
            
            CreateTable(
                "dbo.VolunteerApplicationUsers",
                c => new
                    {
                        Volunteer_Volunteer_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Volunteer_Volunteer_ID, t.ApplicationUser_Id })
                .ForeignKey("dbo.Volunteers", t => t.Volunteer_Volunteer_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Volunteer_Volunteer_ID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Career_JobCareer_Candidate",
                c => new
                    {
                        Career_Job_job_id = c.Int(nullable: false),
                        Career_Candidate_candidate_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Career_Job_job_id, t.Career_Candidate_candidate_id })
                .ForeignKey("dbo.Career_Job", t => t.Career_Job_job_id, cascadeDelete: true)
                .ForeignKey("dbo.Career_Candidate", t => t.Career_Candidate_candidate_id, cascadeDelete: true)
                .Index(t => t.Career_Job_job_id)
                .Index(t => t.Career_Candidate_candidate_id);
            
            CreateTable(
                "dbo.SubscriberNewsletters",
                c => new
                    {
                        Subscriber_subscriber_id = c.Int(nullable: false),
                        Newsletter_newsletter_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subscriber_subscriber_id, t.Newsletter_newsletter_id })
                .ForeignKey("dbo.Subscribers", t => t.Subscriber_subscriber_id, cascadeDelete: true)
                .ForeignKey("dbo.Newsletters", t => t.Newsletter_newsletter_id, cascadeDelete: true)
                .Index(t => t.Subscriber_subscriber_id)
                .Index(t => t.Newsletter_newsletter_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staff_Bios", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Patients", "WishesID", "dbo.Wishes");
            DropForeignKey("dbo.SubscriberNewsletters", "Newsletter_newsletter_id", "dbo.Newsletters");
            DropForeignKey("dbo.SubscriberNewsletters", "Subscriber_subscriber_id", "dbo.Subscribers");
            DropForeignKey("dbo.Feedback_Forms", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Career_JobCareer_Candidate", "Career_Candidate_candidate_id", "dbo.Career_Candidate");
            DropForeignKey("dbo.Career_JobCareer_Candidate", "Career_Job_job_id", "dbo.Career_Job");
            DropForeignKey("dbo.VolunteerApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.VolunteerApplicationUsers", "Volunteer_Volunteer_ID", "dbo.Volunteers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserAppointments", "Appointment_Appointment_ID", "dbo.Appointments");
            DropForeignKey("dbo.ApplicationUserAppointments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SubscriberNewsletters", new[] { "Newsletter_newsletter_id" });
            DropIndex("dbo.SubscriberNewsletters", new[] { "Subscriber_subscriber_id" });
            DropIndex("dbo.Career_JobCareer_Candidate", new[] { "Career_Candidate_candidate_id" });
            DropIndex("dbo.Career_JobCareer_Candidate", new[] { "Career_Job_job_id" });
            DropIndex("dbo.VolunteerApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.VolunteerApplicationUsers", new[] { "Volunteer_Volunteer_ID" });
            DropIndex("dbo.ApplicationUserAppointments", new[] { "Appointment_Appointment_ID" });
            DropIndex("dbo.ApplicationUserAppointments", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Staff_Bios", new[] { "User_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Patients", new[] { "WishesID" });
            DropIndex("dbo.Feedback_Forms", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.SubscriberNewsletters");
            DropTable("dbo.Career_JobCareer_Candidate");
            DropTable("dbo.VolunteerApplicationUsers");
            DropTable("dbo.ApplicationUserAppointments");
            DropTable("dbo.Staff_Bios");
            DropTable("dbo.Slideshows");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Wishes");
            DropTable("dbo.Patients");
            DropTable("dbo.Pages");
            DropTable("dbo.Subscribers");
            DropTable("dbo.Newsletters");
            DropTable("dbo.News");
            DropTable("dbo.Find_Doctor");
            DropTable("dbo.Feedback_Forms");
            DropTable("dbo.Donations");
            DropTable("dbo.Career_Job");
            DropTable("dbo.Career_Candidate");
            DropTable("dbo.Volunteers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Appointments");
            DropTable("dbo.Alerts");
        }
    }
}
