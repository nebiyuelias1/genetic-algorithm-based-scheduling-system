using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public Faculty Faculty { get; set; }
        public int FacultyId { get; set; }
        public List<Section> Sections { get; set; }
        public List<Instructor> Instructors { get; set; }
        public Instructor DepartmentHead { get; set; }
        public int? DepartmentHeadId { get; set; }
    }
}
