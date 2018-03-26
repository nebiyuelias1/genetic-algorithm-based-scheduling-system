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
        public virtual Instructor Instructor { get; set; }
        public int? InstructorId { get; set; }
        public virtual Course Course { get; set; }
        public int CourseId { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
    }
}
