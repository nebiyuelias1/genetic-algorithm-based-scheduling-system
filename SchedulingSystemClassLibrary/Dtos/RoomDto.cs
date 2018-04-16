using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BuildingDto Building { get; set; }
        public int BuildingId { get; set; }
        public int Size { get; set; }
        public bool IsLabRoom { get; set; }
        public bool IsLectureRoom { get; set; }
    }
}
