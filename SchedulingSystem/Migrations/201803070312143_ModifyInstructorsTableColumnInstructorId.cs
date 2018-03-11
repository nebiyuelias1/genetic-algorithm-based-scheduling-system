namespace SchedulingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyInstructorsTableColumnInstructorId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseOfferings", "InstructorId", "dbo.Instructors");
            DropIndex("dbo.CourseOfferings", new[] { "InstructorId" });
            AlterColumn("dbo.CourseOfferings", "InstructorId", c => c.Int());
            CreateIndex("dbo.CourseOfferings", "InstructorId");
            AddForeignKey("dbo.CourseOfferings", "InstructorId", "dbo.Instructors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseOfferings", "InstructorId", "dbo.Instructors");
            DropIndex("dbo.CourseOfferings", new[] { "InstructorId" });
            AlterColumn("dbo.CourseOfferings", "InstructorId", c => c.Int(nullable: false));
            CreateIndex("dbo.CourseOfferings", "InstructorId");
            AddForeignKey("dbo.CourseOfferings", "InstructorId", "dbo.Instructors", "Id", cascadeDelete: true);
        }
    }
}
