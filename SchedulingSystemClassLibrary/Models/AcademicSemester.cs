using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class AcademicSemester
    {
        public int Id { get; set; }
        public byte Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AcademicYear AcademicYear { get; set; }
        public int AcademicYearId { get; set; }
        public bool CurrentSemester { get; set; }

        public string AcademicSemesterTitle { get {
                return Semester == 1 ? "I" : "II";
            }
        }
    }
}
