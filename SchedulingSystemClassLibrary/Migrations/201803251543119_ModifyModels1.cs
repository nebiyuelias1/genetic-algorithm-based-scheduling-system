namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyModels1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Days", "DayNumber", c => c.Byte(nullable: false));
            AlterColumn("dbo.ScheduleEntries", "Period", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ScheduleEntries", "Period", c => c.Int(nullable: false));
            AlterColumn("dbo.Days", "DayNumber", c => c.Int(nullable: false));
        }
    }
}
