namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyRelationshipBetweenRoomsAndSections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InstructorPreferences", "InstructorId", "dbo.Instructors");
            DropIndex("dbo.InstructorPreferences", new[] { "InstructorId" });
            AlterColumn("dbo.Courses", "CourseCode", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Curricula", "FieldOfStudy", c => c.String(nullable: false));
            AlterColumn("dbo.Curricula", "Nomenclature", c => c.String(nullable: false));
            AlterColumn("dbo.Curricula", "AdmissionClassification", c => c.String(nullable: false));
            AlterColumn("dbo.Curricula", "Program", c => c.String(nullable: false));
            AlterColumn("dbo.Sections", "Name", c => c.String(nullable: false));
            DropTable("dbo.InstructorPreferences");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InstructorPreferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstructorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Sections", "Name", c => c.String());
            AlterColumn("dbo.Curricula", "Program", c => c.String());
            AlterColumn("dbo.Curricula", "AdmissionClassification", c => c.String());
            AlterColumn("dbo.Curricula", "Nomenclature", c => c.String());
            AlterColumn("dbo.Curricula", "FieldOfStudy", c => c.String());
            AlterColumn("dbo.Courses", "Title", c => c.String());
            AlterColumn("dbo.Courses", "CourseCode", c => c.String());
            CreateIndex("dbo.InstructorPreferences", "InstructorId");
            AddForeignKey("dbo.InstructorPreferences", "InstructorId", "dbo.Instructors", "Id");
        }
    }
}
