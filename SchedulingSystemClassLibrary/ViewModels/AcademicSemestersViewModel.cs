using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class AcademicSemestersViewModel
    {

        public int Id { get; set; }

        [Required]
        [Range(1, 2)]
        public byte Semester { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public AcademicYear AcademicYear { get; set; }

        [Required]
        [Display(Name = "Academic Year")]
        public int AcademicYearId { get; set; }
        public bool CurrentSemester { get; set; }

        public IEnumerable<AcademicYear> AcademicYears { get; set; }
    }
}
