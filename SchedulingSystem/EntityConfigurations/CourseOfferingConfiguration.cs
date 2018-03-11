using SchedulingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.EntityConfigurations
{
    public class CourseOfferingConfiguration: EntityTypeConfiguration<CourseOffering>
    {
        public CourseOfferingConfiguration()
        {
            this.HasRequired(c => c.Section)
                .WithMany(s => s.CourseOfferings)
                .HasForeignKey(c => c.SectionId)
                .WillCascadeOnDelete(false); 
        }
    }
}
