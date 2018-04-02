using SchedulingSystem.EntityConfigurations;
using SchedulingSystemClassLibrary.Models;
using System.Data.Entity;

namespace SchedulingSystemClassLibrary
{
    public class SchedulingContext : DbContext
    {
        public SchedulingContext() : base("name=SchedulingContext")
        {

        }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<CourseOffering> CourseOfferings { get; set; }
        public virtual DbSet<Curriculum> Curriculums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder); 
        }
    }
}
