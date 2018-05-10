using System;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class AcademicYearDto
    {
        public int Id { get; set; }
        public string GcYear { get; set; }
        public string EtYear { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AcademicYearTitle
        {
            get
            {
                return $"{EtYear} EC({GcYear} GC)";
            }
        }
    }
}