using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class RoomsFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public Building Building { get; set; }

        [Display(Name = "Building")]
        [Required]
        public int BuildingId { get; set; }
        
        [Range(1, 300)]
        [Required]
        [Display(Name = "Room Size")]
        public int Size { get; set; }
        
        [Display(Name = "Is this a Lab room?")]
        public bool IsLabRoom { get; set; }
        [Display(Name = "Is this a Lecture room?")]
        public bool IsLectureRoom { get; set; }
        [Display(Name = "Can this Room be shared?")]
        public bool IsSharedRoom { get; set; }
        public IEnumerable<Building> Buildings { get; set; }

        public RoomsFormViewModel()
        {

        }
        public RoomsFormViewModel(Room room)
        {
            this.Id = room.Id;
            this.Name = room.Name;
            this.BuildingId = room.BuildingId;
            this.Size = room.Size;
            this.IsLabRoom = room.IsLabRoom;
            this.IsLectureRoom = room.IsLectureRoom;
            this.IsSharedRoom = room.IsSharedRoom;
        }
    }
}
