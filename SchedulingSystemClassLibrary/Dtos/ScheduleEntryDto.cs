using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class ScheduleEntryDto
    {
        public int Id { get; set; }
        public CourseDto Course { get; set; }
        public int? CourseId { get; set; }
        public InstructorDto Instructor { get; set; }
        public int? InstructorId { get; set; }
        public RoomDto Room { get; set; }
        public int? RoomId { get; set; }
        public byte Period { get; set; }
        public bool IsLecture { get; set; } = false;
        public bool IsLab { get; set; } = false;
        public bool IsTutor { get; set; } = false;
    }
}
