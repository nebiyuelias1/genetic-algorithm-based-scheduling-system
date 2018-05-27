using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels.DepartmentHead
{
    public class AssignLabRoomViewModel
    {
        public LabGroup LabGroup { get; set; }
        public int LabGroupId { get; set; }
        public List<Room> Rooms { get; set; }

        [Display(Name = "Lab Room")]
        public int LabRoomId { get; set; }
    }
}
