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
        public int Size { get; set; }
        // TODO - should you add IsLab, IsShared properties? 
        [Display(Name = "Is this a Lab room?")]
        public bool IsLabRoom { get; set; }
        [Display(Name = "Is this a Lecture room?")]
        public bool IsLectureRoom { get; set; }
        public IEnumerable<Building> Buildings { get; set; }

        public RoomsFormViewModel()
        {

        }
    }
}
