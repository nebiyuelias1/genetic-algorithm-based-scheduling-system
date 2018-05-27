using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.EntityConfigurations
{
    public class SectionConfiguration : EntityTypeConfiguration<Section>
    {
        public SectionConfiguration()
        {
            this.HasRequired(s => s.Department)
                .WithMany(d => d.Sections)
                .HasForeignKey(s => s.DepartmentId)
                .WillCascadeOnDelete(false);

            this.Ignore(s => s.CurrentYear);

            this.HasMany(s => s.Schedules)
                .WithRequired(s => s.Section)
                .HasForeignKey(s => s.SectionId)
                .WillCascadeOnDelete(false);

            this.HasMany(s => s.Students)
                .WithRequired(s => s.Section)
                .HasForeignKey(s => s.SectionId)
                .WillCascadeOnDelete(false);
        }
    }
}
