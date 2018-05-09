using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class CourseOffering
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public Instructor Instructor { get; set; }
        public int? InstructorId { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public AcademicSemester AcademicSemester { get; set; }
        public int AcademicSemesterId { get; set; }
    }
}
