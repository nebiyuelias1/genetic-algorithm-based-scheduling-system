using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class SectionsFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Entrance Year")]
        public int EntranceYear { get; set; }

        [Required]
        [Display(Name = "Student Count")]
        public int StudentCount { get; set; }
        public Department Department { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public IList<Department> Departments { get; set; }

        public SectionsFormViewModel()
        {

        }

        public SectionsFormViewModel(Section section)
        {
            this.Id = section.Id;
            this.EntranceYear = section.EntranceYear;
            this.DepartmentId = section.DepartmentId;
            this.Name = section.Name;
            this.StudentCount = section.StudentCount; 
        }
    }
}
