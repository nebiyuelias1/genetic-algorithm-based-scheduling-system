namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDatabaseTables2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduleEntries", "Schedule_Id", "dbo.Schedules");
            DropIndex("dbo.ScheduleEntries", new[] { "Schedule_Id" });
            DropColumn("dbo.ScheduleEntries", "Schedule_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScheduleEntries", "Schedule_Id", c => c.Int());
            CreateIndex("dbo.ScheduleEntries", "Schedule_Id");
            AddForeignKey("dbo.ScheduleEntries", "Schedule_Id", "dbo.Schedules", "Id");
        }
    }
}
