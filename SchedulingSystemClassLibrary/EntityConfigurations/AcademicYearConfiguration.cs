using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.EntityConfigurations
{
    public class AcademicYearConfiguration : EntityTypeConfiguration<AcademicYear>
    {
        public AcademicYearConfiguration()
        {
            this.HasMany(y => y.AcademicSemesters)
                .WithRequired(s => s.AcademicYear)
                .HasForeignKey(s => s.AcademicYearId)
                .WillCascadeOnDelete(false);

            this.Ignore(y => y.AcademicYearTitle);
        }
    }
}
