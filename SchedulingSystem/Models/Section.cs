using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EntranceYear { get; set; }
        public int StudentCount { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        // TODO - Which one is better IList, List, IEnumerable
        public virtual List<Room> AssignedRooms { get; set; } = new List<Room>();

        public virtual List<CourseOffering> CourseOfferings { get; set; }
    }
}
