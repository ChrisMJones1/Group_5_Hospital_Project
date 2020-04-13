namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Career_JobCareer_Candidate", "Career_Candidate_candidate_id", "dbo.Career_Candidate");
            DropForeignKey("dbo.Career_JobCareer_Candidate", "Career_Job_job_id", "dbo.Career_Job");
            DropIndex("dbo.Career_JobCareer_Candidate", new[] { "Career_Candidate_candidate_id" });
            DropIndex("dbo.Career_JobCareer_Candidate", new[] { "Career_Job_job_id" });
            DropTable("dbo.Career_JobCareer_Candidate");
        }
    }
}
