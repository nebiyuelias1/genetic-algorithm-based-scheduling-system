namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDaysTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayName = c.String(),
                        DayNumber = c.Int(nullable: false),
                        ScheduleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId)
                .Index(t => t.ScheduleId);
            
            AddColumn("dbo.ScheduleEntries", "DayId", c => c.Int(nullable: false));
            CreateIndex("dbo.ScheduleEntries", "DayId");
            AddForeignKey("dbo.ScheduleEntries", "DayId", "dbo.Days", "Id", cascadeDelete: true);
            DropColumn("dbo.ScheduleEntries", "Day");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScheduleEntries", "Day", c => c.Int(nullable: false));
            DropForeignKey("dbo.Days", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.ScheduleEntries", "DayId", "dbo.Days");
            DropIndex("dbo.Days", new[] { "ScheduleId" });
            DropIndex("dbo.ScheduleEntries", new[] { "DayId" });
            DropColumn("dbo.ScheduleEntries", "DayId");
            DropTable("dbo.Days");
        }
    }
}
