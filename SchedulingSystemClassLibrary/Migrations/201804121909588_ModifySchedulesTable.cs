namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifySchedulesTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Schedules", "MaximumScore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "MaximumScore", c => c.Double(nullable: false));
        }
    }
}
