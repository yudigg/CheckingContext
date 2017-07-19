namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.CheckingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.CheckingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //    
                //context.Roles.AddOrUpdate(
                //p => p.RoleName,
                //   new DataAccess.Role { RoleName = "Super Admin" },
                //   new DataAccess.Role { RoleName = "Admin" },
                //   new DataAccess.Role { RoleName = "Client" }
                // );
        }
    }
}
