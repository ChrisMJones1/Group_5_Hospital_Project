namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class donor1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Career_Jobs", newName: "Career_Job");
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Donations");
            RenameTable(name: "dbo.Career_Job", newName: "Career_Jobs");
        }
    }
}
