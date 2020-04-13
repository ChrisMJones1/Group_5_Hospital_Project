namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class christine_fix_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WishesPatients", "Wishes_Id", "dbo.Wishes");
            DropForeignKey("dbo.WishesPatients", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.WishesPatients", new[] { "Wishes_Id" });
            DropIndex("dbo.WishesPatients", new[] { "Patient_Id" });
            AddColumn("dbo.Patients", "WishesID", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "WishesID");
            AddForeignKey("dbo.Patients", "WishesID", "dbo.Wishes", "Id", cascadeDelete: true);
            DropTable("dbo.WishesPatients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WishesPatients",
                c => new
                    {
                        Wishes_Id = c.Int(nullable: false),
                        Patient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Wishes_Id, t.Patient_Id });
            
            DropForeignKey("dbo.Patients", "WishesID", "dbo.Wishes");
            DropIndex("dbo.Patients", new[] { "WishesID" });
            DropColumn("dbo.Patients", "WishesID");
            CreateIndex("dbo.WishesPatients", "Patient_Id");
            CreateIndex("dbo.WishesPatients", "Wishes_Id");
            AddForeignKey("dbo.WishesPatients", "Patient_Id", "dbo.Patients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WishesPatients", "Wishes_Id", "dbo.Wishes", "Id", cascadeDelete: true);
        }
    }
}
