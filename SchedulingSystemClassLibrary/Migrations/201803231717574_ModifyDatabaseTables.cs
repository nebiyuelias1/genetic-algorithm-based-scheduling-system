namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDatabaseTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "MaximumScore", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "MaximumScore");
        }
    }
}
