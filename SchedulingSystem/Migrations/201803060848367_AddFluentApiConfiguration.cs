namespace SchedulingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFluentApiConfiguration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SectionRooms", newName: "RoomSections");
            DropPrimaryKey("dbo.RoomSections");
            CreateTable(
                "dbo.CourseOfferings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SectionId = c.Int(nullable: false),
                        InstructorId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Semester = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Instructors", t => t.InstructorId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.SectionId)
                .Index(t => t.InstructorId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        FatherName = c.String(),
                        GrandFatherName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddPrimaryKey("dbo.RoomSections", new[] { "Room_Id", "Section_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseOfferings", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.CourseOfferings", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.CourseOfferings", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseOfferings", new[] { "CourseId" });
            DropIndex("dbo.CourseOfferings", new[] { "InstructorId" });
            DropIndex("dbo.CourseOfferings", new[] { "SectionId" });
            DropPrimaryKey("dbo.RoomSections");
            DropTable("dbo.Instructors");
            DropTable("dbo.CourseOfferings");
            AddPrimaryKey("dbo.RoomSections", new[] { "Section_Id", "Room_Id" });
            RenameTable(name: "dbo.RoomSections", newName: "SectionRooms");
        }
    }
}
