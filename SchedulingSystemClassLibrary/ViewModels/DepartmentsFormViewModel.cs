using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class DepartmentsFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [Phone]
        public string Fax { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Faculty Faculty { get; set; }

        [Required]
        [Display(Name = "Faculty")]
        public int FacultyId { get; set; }

        public List<Faculty> Faculties { get; set; }

        public DepartmentsFormViewModel()
        {

        }
        public DepartmentsFormViewModel(Department department)
        {
            this.Name = department.Name;
            this.Phone = department.Phone;
            this.Id = department.Id;
            this.Fax = department.Fax;
            this.FacultyId = department.FacultyId;
            this.Faculty = department.Faculty;
            this.Email = department.Email;
        }
    }
}
