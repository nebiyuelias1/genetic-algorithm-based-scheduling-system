using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class CoursesFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Course Code")]
        public string CourseCode { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public byte Credit { get; set; }

        [Required]
        public byte Lecture { get; set; }

        [Required]
        public byte Laboratory { get; set; }

        [Required]
        public byte Tutor { get; set; }

        [Required]
        [Display(Name = "Delivery Year")]
        public int DeliveryYear { get; set; }

        [Required]
        [Display(Name = "Delivery Semester")]
        public int DeliverySemester { get; set; }

        public Curriculum Curriculum { get; set; }

        [Required]
        [Display(Name = "Curriculum")]
        public int CurriculumId { get; set; }

        [Required]
        [Display(Name = "Pick a Color")]
        public int Color { get; set; }

        [Required]
        public string Acronym { get; set; }

        public IList<Curriculum> Curriculums { get; set; }

        public CoursesFormViewModel()
        {

        }

        public CoursesFormViewModel(Course course)
        {
            this.Id = course.Id;
            this.CourseCode = course.CourseCode;
            this.Credit = course.Credit;
            this.CurriculumId = course.CurriculumId;
            this.DeliverySemester = course.DeliverySemester;
            this.DeliveryYear = course.DeliveryYear;
            this.Laboratory = course.Laboratory;
            this.Lecture = course.Lecture;
            this.Title = course.Title;
            this.Tutor = course.Tutor;
            this.Color = course.Color;
            this.Acronym = course.Acronym;
        }
    }
}
