using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public double Fitness { get; set; }
        public virtual SectionDto Section { get; set; }
        public int SectionId { get; set; }
        public virtual List<DayDto> Days { get; set; }
    }
}
