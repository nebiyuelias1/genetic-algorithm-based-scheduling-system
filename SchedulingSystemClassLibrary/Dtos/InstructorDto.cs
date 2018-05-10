using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class InstructorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public DepartmentDto Department { get; set; }
        public int DepartmentId { get; set; }
        public string AccountId { get; set; }
        public string FullName { get; set; }

    }
}
