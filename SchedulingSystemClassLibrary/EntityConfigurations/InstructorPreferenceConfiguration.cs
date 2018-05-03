using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.EntityConfigurations
{
    public class InstructorPreferenceConfiguration: EntityTypeConfiguration<InstructorPreference>
    {
        public InstructorPreferenceConfiguration()
        {
            this.Ignore(i => i.Preference); 
        }
    }
}
