using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class CourseOfferingsFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Section Section { get; set; }
        [Display(Name = "Section")]
        public int SectionId { get; set; }
        public Instructor Instructor { get; set; }
        [Display(Name = "Instructor")]
        public int? InstructorId { get; set; }
        public Course Course { get; set; }
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }

        public IEnumerable<Instructor> Instructors { get; set; }
    }
}
