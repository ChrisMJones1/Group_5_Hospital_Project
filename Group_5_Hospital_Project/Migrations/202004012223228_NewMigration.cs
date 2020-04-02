namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
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
                "dbo.Find_Doctor",
                c => new
                    {
                        doctor_id = c.Int(nullable: false, identity: true),
                        doctor_name = c.String(),
                        doctor_email = c.String(),
                        doctor_phone = c.Int(nullable: false),
                        doctor_speciality = c.String(),
                        doctor_availabilty = c.String(),
                        doctor_experience = c.String(),
                    })
                .PrimaryKey(t => t.doctor_id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        news_id = c.Int(nullable: false, identity: true),
                        news_title = c.String(),
                        newsposted_date = c.DateTime(nullable: false),
                        newsupdated_date = c.DateTime(nullable: false),
                        news_information = c.String(),
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
                        subscriber_name = c.String(),
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
            DropForeignKey("dbo.SubscriberNewsletters", "Newsletter_newsletter_id", "dbo.Newsletters");
            DropForeignKey("dbo.SubscriberNewsletters", "Subscriber_subscriber_id", "dbo.Subscribers");
            DropIndex("dbo.SubscriberNewsletters", new[] { "Newsletter_newsletter_id" });
            DropIndex("dbo.SubscriberNewsletters", new[] { "Subscriber_subscriber_id" });
            DropTable("dbo.SubscriberNewsletters");
            DropTable("dbo.Pages");
            DropTable("dbo.Subscribers");
            DropTable("dbo.Newsletters");
            DropTable("dbo.News");
            DropTable("dbo.Find_Doctor");
            DropTable("dbo.Donations");
            DropTable("dbo.Career_Job");
            DropTable("dbo.Career_Candidate");
            DropTable("dbo.Alerts");
        }
    }
}
