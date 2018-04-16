using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }

        [Required]
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
        public int DeliveryYear { get; set; }

        [Required]
        public int DeliverySemester { get; set; }
    }
}
