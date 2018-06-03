using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class LabAssistant : Employee
    {
        public Room AssignedLabRoom { get; set; }
        public int? AssignedLabRoomId { get; set; }
    }
}
