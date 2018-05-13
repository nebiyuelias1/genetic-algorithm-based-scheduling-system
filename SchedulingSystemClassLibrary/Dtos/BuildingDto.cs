using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class BuildingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public List<RoomDto> Rooms { get; set; }
        public string BuildingTitle
        {
            get
            {
                return $"{Name} - {Number}";
            }
        }
    }
}
