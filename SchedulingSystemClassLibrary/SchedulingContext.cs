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
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ScheduleEntry> ScheduleEntries { get; set; }
        public virtual DbSet<InstructorPreference> InstructorPreferences { get; set; }
        public virtual DbSet<AcademicSemester> AcademicSemesters { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseOfferingConfiguration());
            modelBuilder.Configurations.Add(new InstructorConfiguration());
            modelBuilder.Configurations.Add(new DayConfiguration());
            modelBuilder.Configurations.Add(new RoomConfiguration());
            modelBuilder.Configurations.Add(new ScheduleConfiguration());
            modelBuilder.Configurations.Add(new InstructorPreferenceConfiguration());
            modelBuilder.Configurations.Add(new SectionConfiguration());

            base.OnModelCreating(modelBuilder); 
        }
    }
}
