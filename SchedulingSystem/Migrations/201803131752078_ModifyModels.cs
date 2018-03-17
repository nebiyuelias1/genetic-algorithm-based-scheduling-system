namespace SchedulingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "ScheduleEntry_Id", "dbo.ScheduleEntries");
            DropForeignKey("dbo.ScheduleEntries", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.ScheduleEntries", "Instructor_Id", "dbo.Instructors");
            DropIndex("dbo.Rooms", new[] { "ScheduleEntry_Id" });
            DropIndex("dbo.ScheduleEntries", new[] { "Course_Id" });
            DropIndex("dbo.ScheduleEntries", new[] { "Instructor_Id" });
            RenameColumn(table: "dbo.ScheduleEntries", name: "Course_Id", newName: "CourseId");
            RenameColumn(table: "dbo.ScheduleEntries", name: "Instructor_Id", newName: "InstructorId");
            AddColumn("dbo.Rooms", "IsLabRoom", c => c.Boolean(nullable: false));
            AddColumn("dbo.Rooms", "IsLectureRoom", c => c.Boolean(nullable: false));
            AddColumn("dbo.ScheduleEntries", "RoomId", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "SectionId", c => c.Int(nullable: false));
            AlterColumn("dbo.ScheduleEntries", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.ScheduleEntries", "InstructorId", c => c.Int(nullable: false));
            CreateIndex("dbo.ScheduleEntries", "CourseId");
            CreateIndex("dbo.ScheduleEntries", "InstructorId");
            CreateIndex("dbo.ScheduleEntries", "RoomId");
            CreateIndex("dbo.Schedules", "SectionId");
            AddForeignKey("dbo.ScheduleEntries", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Schedules", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScheduleEntries", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScheduleEntries", "InstructorId", "dbo.Instructors", "Id", cascadeDelete: true);
            DropColumn("dbo.Rooms", "ScheduleEntry_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "ScheduleEntry_Id", c => c.Int());
            DropForeignKey("dbo.ScheduleEntries", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.ScheduleEntries", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Schedules", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.ScheduleEntries", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Schedules", new[] { "SectionId" });
            DropIndex("dbo.ScheduleEntries", new[] { "RoomId" });
            DropIndex("dbo.ScheduleEntries", new[] { "InstructorId" });
            DropIndex("dbo.ScheduleEntries", new[] { "CourseId" });
            AlterColumn("dbo.ScheduleEntries", "InstructorId", c => c.Int());
            AlterColumn("dbo.ScheduleEntries", "CourseId", c => c.Int());
            DropColumn("dbo.Schedules", "SectionId");
            DropColumn("dbo.ScheduleEntries", "RoomId");
            DropColumn("dbo.Rooms", "IsLectureRoom");
            DropColumn("dbo.Rooms", "IsLabRoom");
            RenameColumn(table: "dbo.ScheduleEntries", name: "InstructorId", newName: "Instructor_Id");
            RenameColumn(table: "dbo.ScheduleEntries", name: "CourseId", newName: "Course_Id");
            CreateIndex("dbo.ScheduleEntries", "Instructor_Id");
            CreateIndex("dbo.ScheduleEntries", "Course_Id");
            CreateIndex("dbo.Rooms", "ScheduleEntry_Id");
            AddForeignKey("dbo.ScheduleEntries", "Instructor_Id", "dbo.Instructors", "Id");
            AddForeignKey("dbo.ScheduleEntries", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Rooms", "ScheduleEntry_Id", "dbo.ScheduleEntries", "Id");
        }
    }
}
