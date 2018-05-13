namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifySchedulesTableAndSectionsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "SectionId", "dbo.Sections");
            AddColumn("dbo.Schedules", "AcademicSemesterId", c => c.Int(nullable: false));
            CreateIndex("dbo.Schedules", "AcademicSemesterId");
            AddForeignKey("dbo.Schedules", "AcademicSemesterId", "dbo.AcademicSemesters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Schedules", "SectionId", "dbo.Sections", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Schedules", "AcademicSemesterId", "dbo.AcademicSemesters");
            DropIndex("dbo.Schedules", new[] { "AcademicSemesterId" });
            DropColumn("dbo.Schedules", "AcademicSemesterId");
            AddForeignKey("dbo.Schedules", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
        }
    }
}
