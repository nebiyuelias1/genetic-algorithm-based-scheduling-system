using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels.DepartmentHead
{
    public class AssignRoomViewModel
    {
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public IEnumerable<Room> Rooms { get; set; }

        [Display(Name = "Lecture Room")]
        public int LectureRoomId { get; set; }

        [Display(Name = "Lecture Room")]
        public int LabRoomId { get; set; }
    }
}
