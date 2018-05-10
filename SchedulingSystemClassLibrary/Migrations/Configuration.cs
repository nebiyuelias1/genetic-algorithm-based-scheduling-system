namespace SchedulingSystemClassLibrary.Migrations
{
    using Models;
    using SchedulingSystemClassLibrary;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchedulingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchedulingContext context)
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

            context.Faculties.AddOrUpdate(f => f.Name,
                new Faculty { Name = "Computing", Email = "computing@dbu.edu.et", Phone = "0116811011", Fax = "0116811014" });


            //context.Departments.AddOrUpdate(d => d.Name,
            //    new Department { Name = "Computer Science", Email = "cs@dbu.edu.et", Phone = "0116811214", Fax = "0116811514", FacultyId = 1 },
            //    new Department { Name = "Information Technology", Email = "it@dbu.edu.et", Phone = "0116811214", Fax = "0116811514", FacultyId = 1 },
            //    new Department { Name = "Information System", Email = "is@dbu.edu.et", Phone = "0116811214", Fax = "0116811514", FacultyId = 1 },
            //    new Department { Name = "Software Engineering", Email = "se@dbu.edu.et", Phone = "0116811214", Fax = "0116811514", FacultyId = 1 });

        }
    }
}
