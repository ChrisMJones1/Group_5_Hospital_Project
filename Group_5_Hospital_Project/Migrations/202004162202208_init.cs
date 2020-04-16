namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserAppointments", newName: "AppointmentApplicationUsers");
            DropPrimaryKey("dbo.AppointmentApplicationUsers");
            AddColumn("dbo.Alerts", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Newsletters", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Subscribers", "User_Id", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.AppointmentApplicationUsers", new[] { "Appointment_Appointment_ID", "ApplicationUser_Id" });
            CreateIndex("dbo.Alerts", "User_Id");
            CreateIndex("dbo.Newsletters", "User_Id");
            CreateIndex("dbo.Subscribers", "User_Id");
            AddForeignKey("dbo.Alerts", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Subscribers", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Newsletters", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Newsletters", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscribers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Alerts", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Subscribers", new[] { "User_Id" });
            DropIndex("dbo.Newsletters", new[] { "User_Id" });
            DropIndex("dbo.Alerts", new[] { "User_Id" });
            DropPrimaryKey("dbo.AppointmentApplicationUsers");
            DropColumn("dbo.Subscribers", "User_Id");
            DropColumn("dbo.Newsletters", "User_Id");
            DropColumn("dbo.Alerts", "User_Id");
            AddPrimaryKey("dbo.AppointmentApplicationUsers", new[] { "ApplicationUser_Id", "Appointment_Appointment_ID" });
            RenameTable(name: "dbo.AppointmentApplicationUsers", newName: "ApplicationUserAppointments");
        }
    }
}
