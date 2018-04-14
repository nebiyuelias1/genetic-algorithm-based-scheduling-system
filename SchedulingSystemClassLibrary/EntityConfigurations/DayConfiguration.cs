using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.EntityConfigurations
{
    public class DayConfiguration : EntityTypeConfiguration<Day>
    {
        public DayConfiguration()
        {
            //this.HasRequired(c => c.Schedule)
            //    .WithMany(s => s.Days)
            //    .HasForeignKey(c => c.ScheduleId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
