namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Find_Doctor", "doctor_specialization", c => c.String());
            AddColumn("dbo.News", "news_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.News", "news_description", c => c.String());
            DropColumn("dbo.Find_Doctor", "doctor_speciality");
            DropColumn("dbo.Find_Doctor", "doctor_availabilty");
            DropColumn("dbo.Find_Doctor", "doctor_experience");
            DropColumn("dbo.News", "newsposted_date");
            DropColumn("dbo.News", "newsupdated_date");
            DropColumn("dbo.News", "news_information");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "news_information", c => c.String());
            AddColumn("dbo.News", "newsupdated_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.News", "newsposted_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Find_Doctor", "doctor_experience", c => c.String());
            AddColumn("dbo.Find_Doctor", "doctor_availabilty", c => c.String());
            AddColumn("dbo.Find_Doctor", "doctor_speciality", c => c.String());
            DropColumn("dbo.News", "news_description");
            DropColumn("dbo.News", "news_date");
            DropColumn("dbo.Find_Doctor", "doctor_specialization");
        }
    }
}
