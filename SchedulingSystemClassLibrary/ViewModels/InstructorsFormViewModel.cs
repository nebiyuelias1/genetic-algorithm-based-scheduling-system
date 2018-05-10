using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchedulingSystemClassLibrary.Models;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class InstructorsFormViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Required]
        [Display(Name = "Grand Father Name")]
        public string GrandFatherName { get; set; }

        public List<Department> Departments { get; set; }
        
        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public InstructorsFormViewModel()
        {

        }
        public InstructorsFormViewModel(Instructor instructor)
        {
            this.FirstName = instructor.FirstName;
            this.FatherName = instructor.FatherName;
            this.GrandFatherName = instructor.GrandFatherName;
            this.DepartmentId = instructor.DepartmentId;
        }
    }
}
