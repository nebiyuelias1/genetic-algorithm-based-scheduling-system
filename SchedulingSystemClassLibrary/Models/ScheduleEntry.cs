using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public int? CourseId { get; set; }
        public Instructor Instructor { get; set; }
        public int? InstructorId { get; set; }
        public Room Room { get; set; }
        public int? RoomId { get; set; }
        // TODO: Should we represent day and period here 
        public Day Day { get; set; }
        public int DayId { get; set; }
        public byte Period { get; set; }
        public bool IsLecture { get; set; } = false;
        public bool IsLab { get; set; } = false;
        public bool IsTutor { get; set; } = false;
        public LabGroup LabGroup { get; set; }
        public int? LabGroupId { get; set; }
    }
}
