using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class InstructorPreference
    {
        public int Id { get; set; }
        public bool[] Preference { get; set; } = new bool[5];
        public Instructor Instructor { get; set; }
        public int InstructorId { get; set; }

    }
}
