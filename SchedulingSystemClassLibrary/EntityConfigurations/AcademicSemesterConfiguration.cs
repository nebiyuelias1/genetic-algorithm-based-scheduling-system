using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.EntityConfigurations
{
    public class AcademicSemesterConfiguration : EntityTypeConfiguration<AcademicSemester>
    {
        public AcademicSemesterConfiguration()
        {
            this.Ignore(s => s.AcademicSemesterTitle);
        }
    }
}
