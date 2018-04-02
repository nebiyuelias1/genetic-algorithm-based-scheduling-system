using SchedulingSystemClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSystemClassLibrary.EntityConfigurations
{
    class RoomConfiguration : EntityTypeConfiguration<Room>
    {
        public RoomConfiguration()
        {
            this.HasMany(r => r.AssignedSections)
                .WithMany(r => r.AssignedRooms);
        }
    }
}
