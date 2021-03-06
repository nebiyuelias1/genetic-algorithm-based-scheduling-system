﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Curriculum
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

        public Department Department { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
    }
}
