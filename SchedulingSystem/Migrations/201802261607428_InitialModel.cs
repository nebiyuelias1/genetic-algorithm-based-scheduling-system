namespace SchedulingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(),
                        Title = c.String(),
                        Credit = c.Byte(nullable: false),
                        Lecture = c.Byte(nullable: false),
                        Laboratory = c.Byte(nullable: false),
                        Tutor = c.Byte(nullable: false),
                        DeliveryYear = c.Int(nullable: false),
                        DeliverySemester = c.Int(nullable: false),
                        CurriculumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curricula", t => t.CurriculumId, cascadeDelete: true)
                .Index(t => t.CurriculumId);
            
            CreateTable(
                "dbo.Curricula",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldOfStudy = c.String(),
                        Nomenclature = c.String(),
                        AdmissionClassification = c.String(),
                        Program = c.String(),
                        StayYear = c.Byte(nullable: false),
                        StaySemester = c.Byte(nullable: false),
                        MinimumCredit = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        FacultyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .Index(t => t.FacultyId);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CurriculumId", "dbo.Curricula");
            DropForeignKey("dbo.Curricula", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Departments", new[] { "FacultyId" });
            DropIndex("dbo.Curricula", new[] { "DepartmentId" });
            DropIndex("dbo.Courses", new[] { "CurriculumId" });
            DropTable("dbo.Faculties");
            DropTable("dbo.Departments");
            DropTable("dbo.Curricula");
            DropTable("dbo.Courses");
        }
    }
}
