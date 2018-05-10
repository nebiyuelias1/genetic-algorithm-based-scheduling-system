using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; 

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
        public Room AssignedLectureRoom { get; set; }
        public int? AssignedLectureRoomId { get; set; }
        public Room AssignedLabRoom { get; set; }
        public int? AssignedLabRoomId { get; set; }
        public List<CourseOffering> CourseOfferings { get; set; }

        public byte CurrentYear {
            get 
            {
                using (var context = new SchedulingContext())
                {
                    var currentSemester = context
                                        .AcademicSemesters
                                        .Include(a => a.AcademicYear)
                                        .Single(s => s.CurrentSemester);

                    return (byte)((int.Parse(currentSemester.AcademicYear.EtYear) - this.EntranceYear) + 1);
                }

            }
        }
    }
}
