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

            this.HasMany(r => r.AssignedLabGroups)
                 .WithOptional(r => r.Room)
                 .HasForeignKey(s => s.RoomId)
                 .WillCascadeOnDelete(false);

            this.HasRequired(r => r.Building)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BuildingId)
                .WillCascadeOnDelete(false);

            //this.HasOptional(r => r.LabAssistance)
            //    .WithOptionalPrincipal(l => l.Room)
            //    .Map(x => x.MapKey("RoomId"));

            this.Ignore(r => r.Title);
        }
    }
}
