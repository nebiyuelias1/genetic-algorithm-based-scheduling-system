using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class InstructorPreference
    {
        public int Id { get; set; }
        public bool[] Preference { get; set; } = new bool[5];

    }
}
