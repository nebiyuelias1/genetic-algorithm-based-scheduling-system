namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDaysTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Days", new[] { "ScheduleId" });
            RenameColumn(table: "dbo.Days", name: "ScheduleId", newName: "Schedule_Id");
            AlterColumn("dbo.Days", "Schedule_Id", c => c.Int());
            CreateIndex("dbo.Days", "Schedule_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Days", new[] { "Schedule_Id" });
            AlterColumn("dbo.Days", "Schedule_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Days", name: "Schedule_Id", newName: "ScheduleId");
            CreateIndex("dbo.Days", "ScheduleId");
        }
    }
}
