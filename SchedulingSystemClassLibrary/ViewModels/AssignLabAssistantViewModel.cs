using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class AssignLabAssistantViewModel
    {
        public LabAssistant LabAssistant { get; set; }
        public List<Room> Rooms { get; set; }
        public int LabAssistantId { get; set; }

        [Required]
        [Display(Name = "Lab Room")]
        public int RoomId { get; set; }
    }
}
