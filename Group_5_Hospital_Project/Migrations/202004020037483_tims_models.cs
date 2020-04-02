namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tims_models : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Appointment_Appointment_ID", "dbo.Appointments");
            DropForeignKey("dbo.AspNetUsers", "Volunteer_Volunteer_ID", "dbo.Volunteers");
            DropIndex("dbo.AspNetUsers", new[] { "Appointment_Appointment_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Volunteer_Volunteer_ID" });
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
            
            DropColumn("dbo.AspNetUsers", "Appointment_Appointment_ID");
            DropColumn("dbo.AspNetUsers", "Volunteer_Volunteer_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Volunteer_Volunteer_ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Appointment_Appointment_ID", c => c.Int());
            DropForeignKey("dbo.VolunteerApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.VolunteerApplicationUsers", "Volunteer_Volunteer_ID", "dbo.Volunteers");
            DropForeignKey("dbo.ApplicationUserAppointments", "Appointment_Appointment_ID", "dbo.Appointments");
            DropForeignKey("dbo.ApplicationUserAppointments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.VolunteerApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.VolunteerApplicationUsers", new[] { "Volunteer_Volunteer_ID" });
            DropIndex("dbo.ApplicationUserAppointments", new[] { "Appointment_Appointment_ID" });
            DropIndex("dbo.ApplicationUserAppointments", new[] { "ApplicationUser_Id" });
            DropTable("dbo.VolunteerApplicationUsers");
            DropTable("dbo.ApplicationUserAppointments");
            CreateIndex("dbo.AspNetUsers", "Volunteer_Volunteer_ID");
            CreateIndex("dbo.AspNetUsers", "Appointment_Appointment_ID");
            AddForeignKey("dbo.AspNetUsers", "Volunteer_Volunteer_ID", "dbo.Volunteers", "Volunteer_ID");
            AddForeignKey("dbo.AspNetUsers", "Appointment_Appointment_ID", "dbo.Appointments", "Appointment_ID");
        }
    }
}
