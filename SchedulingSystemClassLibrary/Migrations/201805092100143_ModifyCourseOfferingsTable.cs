namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCourseOfferingsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseOfferings", "AcademicSemesterId", c => c.Int(nullable: false));
            CreateIndex("dbo.CourseOfferings", "AcademicSemesterId");
            AddForeignKey("dbo.CourseOfferings", "AcademicSemesterId", "dbo.AcademicSemesters", "Id");
            DropColumn("dbo.CourseOfferings", "Year");
            DropColumn("dbo.CourseOfferings", "Semester");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseOfferings", "Semester", c => c.Int(nullable: false));
            AddColumn("dbo.CourseOfferings", "Year", c => c.Int(nullable: false));
            DropForeignKey("dbo.CourseOfferings", "AcademicSemesterId", "dbo.AcademicSemesters");
            DropIndex("dbo.CourseOfferings", new[] { "AcademicSemesterId" });
            DropColumn("dbo.CourseOfferings", "AcademicSemesterId");
        }
    }
}
