namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchedulesAndScheduleEntriesTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        IsLecture = c.Boolean(nullable: false),
                        IsLab = c.Boolean(nullable: false),
                        IsTutor = c.Boolean(nullable: false),
                        Course_Id = c.Int(),
                        Instructor_Id = c.Int(),
                        Schedule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Instructors", t => t.Instructor_Id)
                .ForeignKey("dbo.Schedules", t => t.Schedule_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Instructor_Id)
                .Index(t => t.Schedule_Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fitness = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Rooms", "ScheduleEntry_Id", c => c.Int());
            CreateIndex("dbo.Rooms", "ScheduleEntry_Id");
            AddForeignKey("dbo.Rooms", "ScheduleEntry_Id", "dbo.ScheduleEntries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleEntries", "Schedule_Id", "dbo.Schedules");
            DropForeignKey("dbo.Rooms", "ScheduleEntry_Id", "dbo.ScheduleEntries");
            DropForeignKey("dbo.ScheduleEntries", "Instructor_Id", "dbo.Instructors");
            DropForeignKey("dbo.ScheduleEntries", "Course_Id", "dbo.Courses");
            DropIndex("dbo.ScheduleEntries", new[] { "Schedule_Id" });
            DropIndex("dbo.ScheduleEntries", new[] { "Instructor_Id" });
            DropIndex("dbo.ScheduleEntries", new[] { "Course_Id" });
            DropIndex("dbo.Rooms", new[] { "ScheduleEntry_Id" });
            DropColumn("dbo.Rooms", "ScheduleEntry_Id");
            DropTable("dbo.Schedules");
            DropTable("dbo.ScheduleEntries");
        }
    }
}
