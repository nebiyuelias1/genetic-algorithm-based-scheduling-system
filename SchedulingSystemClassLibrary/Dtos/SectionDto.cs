using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class SectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EntranceYear { get; set; }
        public int StudentCount { get; set; }
        public DepartmentDto Department { get; set; }
        public int DepartmentId { get; set; }
        public int? AssignedLectureRoomId { get; set; }
        public int? AssignedLabRoomId { get; set; }
    }
}
