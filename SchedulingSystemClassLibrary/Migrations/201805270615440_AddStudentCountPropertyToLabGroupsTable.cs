namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentCountPropertyToLabGroupsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LabGroups", "StudentCount", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LabGroups", "StudentCount");
        }
    }
}
