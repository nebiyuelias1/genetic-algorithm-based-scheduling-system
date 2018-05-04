using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class InstructorCourseOfferingsViewModel
    {
        public List<CourseOffering> CourseOfferings { get; set; }
        public string InstructorFullName { get; set; }
    }
}
