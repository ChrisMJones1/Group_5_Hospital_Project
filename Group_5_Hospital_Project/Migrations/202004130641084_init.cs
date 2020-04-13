namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
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
            DropForeignKey("dbo.Career_JobCareer_Candidate", "Career_Candidate_candidate_id", "dbo.Career_Candidate");
            DropForeignKey("dbo.Career_JobCareer_Candidate", "Career_Job_job_id", "dbo.Career_Job");
            DropIndex("dbo.Career_JobCareer_Candidate", new[] { "Career_Candidate_candidate_id" });
            DropIndex("dbo.Career_JobCareer_Candidate", new[] { "Career_Job_job_id" });
            DropColumn("dbo.News", "news_description");
            DropColumn("dbo.News", "news_date");
            DropColumn("dbo.Find_Doctor", "doctor_specialization");
            DropTable("dbo.Career_JobCareer_Candidate");
        }
    }
}
