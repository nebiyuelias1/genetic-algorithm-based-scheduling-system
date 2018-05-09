using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class CourseOfferingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SectionDto Section { get; set; }
        public int SectionId { get; set; }
        public InstructorDto Instructor { get; set; }
        public int? InstructorId { get; set; }
        public CourseDto Course { get; set; }
        public int CourseId { get; set; }
        public AcademicSemesterDto AcademicSemester { get; set; }
        public int AcademicSemesterId { get; set; }
    }
}
