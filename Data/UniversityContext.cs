using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniversityProject_2.Models;
namespace UniversityProject_2.Data
{
    public class UniversityContext:DbContext
    {
        public UniversityContext():base("UniversityContext")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<UniversityCampus> UniversityCampuses { get; set; }
    }
}