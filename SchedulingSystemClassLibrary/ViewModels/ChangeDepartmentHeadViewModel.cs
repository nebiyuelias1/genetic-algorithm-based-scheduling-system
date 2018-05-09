using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class ChangeDepartmentHeadViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Instructor> Instructors { get; set; }

        [Required]
        [Display(Name = "Choose Department Head")]
        public int? DepartmentHeadId { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public Instructor OldDepartmentHead { get; set; }
        public string OldDepartmentHeadAccountId { get; set; }
    }
}
