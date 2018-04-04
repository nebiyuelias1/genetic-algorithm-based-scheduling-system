using SchedulingSystemClassLibrary.EntityConfigurations;
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
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseOfferingConfiguration());
            modelBuilder.Configurations.Add(new InstructorConfiguration());
            modelBuilder.Configurations.Add(new DayConfiguration());
            modelBuilder.Configurations.Add(new RoomConfiguration()); 

            base.OnModelCreating(modelBuilder); 
        }
    }
}
