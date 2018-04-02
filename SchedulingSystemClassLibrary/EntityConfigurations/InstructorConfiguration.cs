using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.EntityConfigurations
{
    public class InstructorConfiguration: EntityTypeConfiguration<Instructor>
    {
        public InstructorConfiguration()
        {
            this.HasRequired(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DepartmentId)
                .WillCascadeOnDelete(false);

            
        }
    }
}
