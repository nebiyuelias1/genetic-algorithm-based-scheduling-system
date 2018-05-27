using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Student : Employee
    {
        public Section Section { get; set; }
        public int SectionId { get; set; }
    }
}
