using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Dtos
{
    public class DayDto
    {
        public int Id { get; set; }
        public string DayName { get; set; }
        public byte DayNumber { get; set; }
        public virtual List<ScheduleEntryDto> Periods { get; set; }
    }
}
