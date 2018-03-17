using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class Course
    {
        // Determine whether this course is a Lab course or not
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public byte Credit { get; set; }
        public byte Lecture { get; set; }
        public byte Laboratory { get; set; }
        public byte Tutor { get; set; }
        public int DeliveryYear { get; set; }
        public int DeliverySemester { get; set; }
        public Curriculum Curriculum { get; set; }
        public int CurriculumId { get; set; }
    }
}
