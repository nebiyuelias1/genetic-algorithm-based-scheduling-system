namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyBuildingsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Buildings", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Buildings", "Name", c => c.Int(nullable: false));
        }
    }
}
