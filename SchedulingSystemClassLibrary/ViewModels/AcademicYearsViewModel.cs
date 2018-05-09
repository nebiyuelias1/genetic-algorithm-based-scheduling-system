using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class AcademicYearsViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Gregorian Calendar Year")]
        public string GcYear { get; set; }

        [Required]
        [Display(Name = "Ethiopian Calendar Year")]
        public string EtYear { get; set; }
        
        [Required]
        [Display(Name = "Academic Year Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Academic Year End Date")]
        public DateTime EndDate { get; set; }
    }
}
