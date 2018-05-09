using System;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class AcademicSemesterDto
    {
        public int Id { get; set; }
        public byte Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AcademicYearDto AcademicYear { get; set; }
        public int AcademicYearId { get; set; }
        public bool CurrentSemester { get; set; }

        public string AcademicSemesterTitle
        {
            get
            {
                return Semester == 1 ? "I" : "II";
            }
        }
    }
}