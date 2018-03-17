using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchedulingSystem.Models;

namespace SchedulingSystem.EntityConfigurations
{
    public class ScheduleConfiguration : EntityTypeConfiguration<Schedule>
    {
        public ScheduleConfiguration()
        {

        }
    }
}
