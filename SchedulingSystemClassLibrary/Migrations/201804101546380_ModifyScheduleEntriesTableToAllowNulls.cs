namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyScheduleEntriesTableToAllowNulls : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduleEntries", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ScheduleEntries", "InstructorId", "dbo.Instructors");
            DropIndex("dbo.ScheduleEntries", new[] { "CourseId" });
            DropIndex("dbo.ScheduleEntries", new[] { "InstructorId" });
            AlterColumn("dbo.ScheduleEntries", "CourseId", c => c.Int());
            AlterColumn("dbo.ScheduleEntries", "InstructorId", c => c.Int());
            CreateIndex("dbo.ScheduleEntries", "CourseId");
            CreateIndex("dbo.ScheduleEntries", "InstructorId");
            AddForeignKey("dbo.ScheduleEntries", "CourseId", "dbo.Courses", "Id");
            AddForeignKey("dbo.ScheduleEntries", "InstructorId", "dbo.Instructors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleEntries", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.ScheduleEntries", "CourseId", "dbo.Courses");
            DropIndex("dbo.ScheduleEntries", new[] { "InstructorId" });
            DropIndex("dbo.ScheduleEntries", new[] { "CourseId" });
            AlterColumn("dbo.ScheduleEntries", "InstructorId", c => c.Int(nullable: false));
            AlterColumn("dbo.ScheduleEntries", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.ScheduleEntries", "InstructorId");
            CreateIndex("dbo.ScheduleEntries", "CourseId");
            AddForeignKey("dbo.ScheduleEntries", "InstructorId", "dbo.Instructors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScheduleEntries", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
