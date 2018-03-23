using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        public virtual Course Course { get; set; }
        public int CourseId { get; set; }
        public virtual Instructor Instructor { get; set; }
        public int InstructorId { get; set; }
        public virtual Room Room { get; set; }
        public int RoomId { get; set; }
        // TODO: Should we represent day and period here 
        public virtual Day Day { get; set; }
        public int DayId { get; set; }
        public int Period { get; set; }
        public bool IsLecture { get; set; } = false;
        public bool IsLab { get; set; } = false;
        public bool IsTutor { get; set; } = false; 
    }
}
