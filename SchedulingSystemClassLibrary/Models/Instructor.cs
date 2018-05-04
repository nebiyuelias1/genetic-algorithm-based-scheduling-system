using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        //public Employee Employee { get; set; }
        //public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public List<CourseOffering> CurrentlyAssignedCourses { get; set; } = new List<CourseOffering>();
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public InstructorPreference InstructorPreference { get; set; }
        public int? InstructorPreferenceId { get; set; }
        public string AccountId { get; set; }

        public string FullName { get {

                return $"{FirstName} {FatherName} {GrandFatherName}";
            } }

    }
}
