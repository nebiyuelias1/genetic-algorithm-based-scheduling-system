using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Section
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Entrance Year")]
        public int EntranceYear { get; set; }

        [Required]
        [Display(Name = "Student Count")]
        public int StudentCount { get; set; }
        public Department Department { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        // TODO - Which one is better IList, List, IEnumerable
        public virtual List<Room> AssignedRooms { get; set; } = new List<Room>();

        public virtual List<CourseOffering> CourseOfferings { get; set; }
    }
}
