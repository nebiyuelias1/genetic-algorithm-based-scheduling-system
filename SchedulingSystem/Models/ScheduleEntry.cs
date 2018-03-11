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
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
        public List<Room> Rooms { get; set; }
        // TODO: Should we represent day and period here 
        //public int Day { get; set; }
        //public int Period { get; set; }
        public bool IsLecture { get; set; } = false;
        public bool IsLab { get; set; } = false;
        public bool IsTutor { get; set; } = false; 
    }
}
