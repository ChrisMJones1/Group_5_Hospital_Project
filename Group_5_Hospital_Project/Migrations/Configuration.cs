namespace Group_5_Hospital_Project.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Group_5_Hospital_Project.Data.Group_5_Hospital_Project_Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Group_5_Hospital_Project.Data.Group_5_Hospital_Project_Context";
        }

        protected override void Seed(Group_5_Hospital_Project.Data.Group_5_Hospital_Project_Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
