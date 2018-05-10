using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels.DepartmentHead
{
    public class DepartmentHeadHomeViewModel
    {
        public int CoursesCount { get; set; }
        public int DepartmentsCount { get; set; }
        public int SectionsCount { get; set; }
        public int BuildingsCount { get; set; }
        public int InstructorsCount { get; set; }
        public int RoomsCount { get; set; }
        public int CourseOfferingsCount { get; set; }
        public AcademicSemester Semester { get; set; }
        public Department Department { get; set; }
    }
}
