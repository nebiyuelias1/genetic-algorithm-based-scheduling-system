using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public List<LabGroup> AssignedLabGroups { get; set; }
        public bool IsLabRoom { get; set; }
        public bool IsLectureRoom { get; set; }
        public bool IsSharedRoom { get; set; }
        public string Title
        {
            get
            {
                return $"{Name} in {Building.BuildingTitle}";
            }
        }
    }
}
