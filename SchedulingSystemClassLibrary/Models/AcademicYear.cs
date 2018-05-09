using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public string GcYear { get; set; }
        public string EtYear { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AcademicSemester> AcademicSemesters { get; set; }
        public string AcademicYearTitle { get
            {
                return $"{EtYear} EC({GcYear} GC)";
            }
        }
    }
}
