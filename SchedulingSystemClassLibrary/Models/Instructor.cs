﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Instructor : Employee
    {
        public List<CourseOffering> CurrentlyAssignedCourses { get; set; } = new List<CourseOffering>();
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public InstructorPreference InstructorPreference { get; set; }
        public int? InstructorPreferenceId { get; set; }

        

    }
}
