using SchedulingSystemClassLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.Models
{
    public class ScheduleEntrySwapModel
    {
        public ScheduleEntryDto PeriodA { get; set; }

        public int PeriodADayNumber { get; set; }

        public ScheduleEntryDto PeriodB { get; set; }

        public int PeriodBDayNumber { get; set; }
    }
}
