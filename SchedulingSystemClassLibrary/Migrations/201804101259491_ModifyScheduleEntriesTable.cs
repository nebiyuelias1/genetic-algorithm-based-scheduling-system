namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyScheduleEntriesTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduleEntries", "RoomId", "dbo.Rooms");
            DropIndex("dbo.ScheduleEntries", new[] { "RoomId" });
            AlterColumn("dbo.ScheduleEntries", "RoomId", c => c.Int());
            CreateIndex("dbo.ScheduleEntries", "RoomId");
            AddForeignKey("dbo.ScheduleEntries", "RoomId", "dbo.Rooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleEntries", "RoomId", "dbo.Rooms");
            DropIndex("dbo.ScheduleEntries", new[] { "RoomId" });
            AlterColumn("dbo.ScheduleEntries", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.ScheduleEntries", "RoomId");
            AddForeignKey("dbo.ScheduleEntries", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
    }
}
