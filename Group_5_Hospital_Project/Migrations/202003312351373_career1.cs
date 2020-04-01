namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class career1 : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Career_Jobs",
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Career_Jobs");
            DropTable("dbo.Career_Candidate");
        }
    }
}
