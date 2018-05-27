namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRelationshipBetweenSchedulesAndDaysTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Days", "Schedule_Id", "dbo.Schedules");
            DropIndex("dbo.Days", new[] { "Schedule_Id" });
            RenameColumn(table: "dbo.Days", name: "Schedule_Id", newName: "ScheduleId");
            AlterColumn("dbo.Days", "ScheduleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Days", "ScheduleId");
            AddForeignKey("dbo.Days", "ScheduleId", "dbo.Schedules", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Days", "ScheduleId", "dbo.Schedules");
            DropIndex("dbo.Days", new[] { "ScheduleId" });
            AlterColumn("dbo.Days", "ScheduleId", c => c.Int());
            RenameColumn(table: "dbo.Days", name: "ScheduleId", newName: "Schedule_Id");
            CreateIndex("dbo.Days", "Schedule_Id");
            AddForeignKey("dbo.Days", "Schedule_Id", "dbo.Schedules", "Id");
        }
    }
}
