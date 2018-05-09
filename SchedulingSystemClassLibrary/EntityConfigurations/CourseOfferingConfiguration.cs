using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.EntityConfigurations
{
    public class CourseOfferingConfiguration: EntityTypeConfiguration<CourseOffering>
    {
        public CourseOfferingConfiguration()
        {
            this.HasRequired(c => c.Section)
                .WithMany(s => s.CourseOfferings)
                .HasForeignKey(c => c.SectionId)
                .WillCascadeOnDelete(false);

            this.HasRequired(c => c.AcademicSemester)
                .WithMany(a => a.CourseOfferings)
                .HasForeignKey(c => c.AcademicSemesterId)
                .WillCascadeOnDelete(false); 
        }
    }
}
