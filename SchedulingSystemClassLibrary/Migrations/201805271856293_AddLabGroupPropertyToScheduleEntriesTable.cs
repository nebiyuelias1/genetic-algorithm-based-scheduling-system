namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLabGroupPropertyToScheduleEntriesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleEntries", "LabGroupId", c => c.Int());
            CreateIndex("dbo.ScheduleEntries", "LabGroupId");
            AddForeignKey("dbo.ScheduleEntries", "LabGroupId", "dbo.LabGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleEntries", "LabGroupId", "dbo.LabGroups");
            DropIndex("dbo.ScheduleEntries", new[] { "LabGroupId" });
            DropColumn("dbo.ScheduleEntries", "LabGroupId");
        }
    }
}
