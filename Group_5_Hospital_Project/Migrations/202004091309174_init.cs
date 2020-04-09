namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Diagnosis = c.String(),
                        RoomNumber = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Send_Best_Wishes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SenderName = c.String(),
                        Status = c.String(),
                        Message = c.String(),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.Patient_Id);
            
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
            
            DropColumn("dbo.Subscribers", "subscriber_name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscribers", "subscriber_name", c => c.String());
            DropForeignKey("dbo.Send_Best_Wishes", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.Send_Best_Wishes", new[] { "Patient_Id" });
            DropTable("dbo.Slideshows");
            DropTable("dbo.Send_Best_Wishes");
            DropTable("dbo.Patients");
        }
    }
}
