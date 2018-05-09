using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class CurriculumsFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Field of Study")]
        public string FieldOfStudy { get; set; }

        [Required]
        public string Nomenclature { get; set; }

        [Required]
        [Display(Name = "Admission Classification")]
        public string AdmissionClassification { get; set; }

        [Required]
        public string Program { get; set; }

        [Required]
        [Display(Name = "Stay Year")]
        public byte StayYear { get; set; }

        [Required]
        [Display(Name = "Stay Semester")]
        public byte StaySemester { get; set; }

        [Required]
        [Display(Name = "Minimum Credit")]
        public int MinimumCredit { get; set; }


        public CurriculumsFormViewModel(Curriculum curriculum)
        {
            this.Id = curriculum.Id; 
            this.Nomenclature = curriculum.Nomenclature;
            this.Program = curriculum.Program;
            this.StaySemester = curriculum.StaySemester;
            this.StayYear = curriculum.StayYear;
            this.FieldOfStudy = curriculum.FieldOfStudy;
            this.AdmissionClassification = curriculum.AdmissionClassification;
            this.MinimumCredit = curriculum.MinimumCredit;
        }

        public CurriculumsFormViewModel()
        {
        }
    }
}
