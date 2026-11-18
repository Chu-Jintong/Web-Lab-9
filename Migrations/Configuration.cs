namespace UniversityProject_2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UniversityProject_2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversityProject_2.Data.UniversityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "UniversityProject_2.Data.UniversityContext";
        }

        protected override void Seed(UniversityProject_2.Data.UniversityContext context)
        {
            context.UniversityCampuses.AddOrUpdate(
                c => c.Name,
                new Models.UniversityCampus
                {
                    Name = "Main Campus"
                },
                new UniversityCampus
                {
                    Name = "North Campus"
                },
                new UniversityCampus
                {
                    Name = "South Campus"
                }
                );
            context.SaveChanges();
            var mainCampus = context.UniversityCampuses.First(c => c.Name == "Main Campus");
            var northCampus = context.UniversityCampuses.First(c => c.Name == "North Campus");
            var southCampus = context.UniversityCampuses.First(c => c.Name == "South Campus");
            context.Students.AddOrUpdate
                (
                    s => s.Name,
                    new Student
                    {
                        Name = "Edward Cullen",
                        Address = "No.1 Main Street",
                        CampusID = mainCampus.ID
                    },
                    new Student
                    {
                        Name = "Tina Chu",
                        Address = "No.11 Main Street",
                        CampusID = mainCampus.ID
                    },
                    new Student
                    {
                        Name = "Mickey Mouse",
                        Address = "No.Mouse Main Street",
                        CampusID = northCampus.ID
                    },
                    new Student
                    {
                        Name="Mini Miller",
                        Address="30 South Road",
                        CampusID=southCampus.ID
                    },
                    new Student
                    {
                        Name="David Big",
                        Address="16 Lake Road",
                        CampusID=northCampus.ID//Get the main key
                    }
                );
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
