using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.ViewModels
{
    public class DepartmentInstructorsViewModel
    {
        public List<Instructor> Instructors { get; set; }
        public string DepartmentName { get; set; }
    }
}
