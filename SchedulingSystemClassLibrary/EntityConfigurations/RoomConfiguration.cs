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
            this.HasMany(r => r.AssignedLectureSections)
                .WithOptional(r => r.AssignedLectureRoom)
                .HasForeignKey(s => s.AssignedLectureRoomId)
                .WillCascadeOnDelete(false);

            this.HasMany(r => r.AssignedLabSections)
                 .WithOptional(r => r.AssignedLabRoom)
                 .HasForeignKey(s => s.AssignedLabRoomId)
                 .WillCascadeOnDelete(false);

            this.HasRequired(r => r.Building)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BuildingId)
                .WillCascadeOnDelete(false);
        }
    }
}
