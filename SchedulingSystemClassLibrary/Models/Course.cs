using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Course
    {
        // Determine whether this course is a Lab course or not
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
    }
}
