namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAcronymPropertyToCoursesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Acronym", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Acronym");
        }
    }
}
