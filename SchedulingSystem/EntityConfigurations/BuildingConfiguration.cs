using SchedulingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystem.EntityConfigurations
{
    public class BuildingConfiguration : EntityTypeConfiguration<Building>
    {
        public BuildingConfiguration()
        {
            //this.HasMany(b => b.Rooms)
            //    .WithRequired(r => r.Building)
            //    .HasForeignKey(b => b.BuildingId)
            //    .WillCascadeOnDelete(false); 

            
        }
        
    }
}
