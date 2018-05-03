using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SectionDtoMinimal> Sections { get; set; }
        public List<InstructorDtoMinimal> Instructors { get; set; }
        public InstructorDto DepartmentHead { get; set; }
        public int? DepartmentHeadId { get; set; }
    }
}
