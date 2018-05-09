namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAcademicYearAndSemesterTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicSemesters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Semester = c.Byte(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        AcademicYearId = c.Int(nullable: false),
                        CurrentSemester = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .Index(t => t.AcademicYearId);
            
            CreateTable(
                "dbo.AcademicYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GcYear = c.Int(nullable: false),
                        EtYear = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AcademicSemesters", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.AcademicSemesters", new[] { "AcademicYearId" });
            DropTable("dbo.AcademicYears");
            DropTable("dbo.AcademicSemesters");
        }
    }
}
