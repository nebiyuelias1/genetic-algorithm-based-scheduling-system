using SchedulingSystem.EntityConfigurations;
using SchedulingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem
{
    class SchedulingContext : DbContext
    {
        public SchedulingContext() : base("name=SchedulingContext")
        {

        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Curriculum> Curriculums { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<CourseOffering> CourseOfferings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseOfferingConfiguration());
            modelBuilder.Configurations.Add(new InstructorConfiguration()); 

            base.OnModelCreating(modelBuilder); 
        }
    }
}
