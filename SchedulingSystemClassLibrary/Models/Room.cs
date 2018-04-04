using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public Building Building { get; set; }


        [Display(Name="Building")]
        [Required]
        public int BuildingId { get; set; }
        public int Size { get; set; }
        public List<Section> AssignedLectureSections { get; set; }
        public List<Section> AssignedLabSections { get; set; }
        // TODO - should you add IsLab, IsShared properties? 
        public bool IsLabRoom { get; set; }
        public bool IsLectureRoom { get; set; }
    }
}
