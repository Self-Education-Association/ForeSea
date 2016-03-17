namespace LST.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LST.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "LST.Models.ApplicationDbContext";
        }

        protected override void Seed(LST.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.TestMatches.AddOrUpdate(
                new Models.TestMatch
                {
                    Id = Guid.Empty,
                    Name = "Listening and Speaking Test - 1st",
                    Limit = 900,
                    Visible = false,
                    StartTime = new DateTime(2015, 12, 31, 0, 0, 0),
                    EndTime = new DateTime(2015, 12, 31, 23, 59, 59)
                });
        }
    }
}
