namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
            DropTable("dbo.Find_Doctor");
        }
    }
}
