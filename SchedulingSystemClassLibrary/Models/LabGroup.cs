using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class LabGroup
    {
        public int Id { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public string Name { get; set; }
        public Room Room { get; set; }
        public int? RoomId { get; set; }
        public byte StudentCount { get; set; }
    }
}
