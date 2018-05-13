using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.Models
{
    public class Building
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Number { get; set; }
        public List<Room> Rooms { get; set; }
        public string Title {
            get
            {
                return $"{Name}-{Number}";
            }
        }
    }
}
