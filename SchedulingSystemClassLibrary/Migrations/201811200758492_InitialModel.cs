namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        Color = c.Int(nullable: false),
                        IsFullDay = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        GcYear = c.String(),
                        EtYear = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseOfferings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SectionId = c.Int(nullable: false),
                        InstructorId = c.Int(),
                        CourseId = c.Int(nullable: false),
                        AcademicSemesterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicSemesters", t => t.AcademicSemesterId)
                .ForeignKey("dbo.Instructors", t => t.InstructorId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.SectionId)
                .Index(t => t.InstructorId)
                .Index(t => t.CourseId)
                .Index(t => t.AcademicSemesterId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Credit = c.Byte(nullable: false),
                        Lecture = c.Byte(nullable: false),
                        Laboratory = c.Byte(nullable: false),
                        Tutor = c.Byte(nullable: false),
                        DeliveryYear = c.Int(nullable: false),
                        DeliverySemester = c.Int(nullable: false),
                        CurriculumId = c.Int(nullable: false),
                        Color = c.Int(nullable: false),
                        Acronym = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curricula", t => t.CurriculumId, cascadeDelete: true)
                .Index(t => t.CurriculumId);
            
            CreateTable(
                "dbo.Curricula",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldOfStudy = c.String(nullable: false),
                        Nomenclature = c.String(nullable: false),
                        AdmissionClassification = c.String(nullable: false),
                        Program = c.String(nullable: false),
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
                        DepartmentHeadId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructors", t => t.DepartmentHeadId)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .Index(t => t.FacultyId)
                .Index(t => t.DepartmentHeadId);
            
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        InstructorPreferenceId = c.Int(),
                        FirstName = c.String(),
                        FatherName = c.String(),
                        GrandFatherName = c.String(),
                        AccountId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.InstructorPreferences",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        InstructorId = c.Int(nullable: false),
                        PreferenceData = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructors", t => t.Id)
                .Index(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EntranceYear = c.Int(nullable: false),
                        StudentCount = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        AssignedLectureRoomId = c.Int(),
                        AssignedLabRoomId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.AssignedLectureRoomId)
                .ForeignKey("dbo.Rooms", t => t.AssignedLabRoomId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId)
                .Index(t => t.AssignedLectureRoomId)
                .Index(t => t.AssignedLabRoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BuildingId = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                        IsLabRoom = c.Boolean(nullable: false),
                        IsLectureRoom = c.Boolean(nullable: false),
                        IsSharedRoom = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.LabGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        Name = c.String(),
                        RoomId = c.Int(),
                        StudentCount = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.SectionId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fitness = c.Double(nullable: false),
                        SectionId = c.Int(nullable: false),
                        AcademicSemesterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicSemesters", t => t.AcademicSemesterId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.SectionId)
                .Index(t => t.AcademicSemesterId);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayName = c.String(),
                        DayNumber = c.Byte(nullable: false),
                        ScheduleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: true)
                .Index(t => t.ScheduleId);
            
            CreateTable(
                "dbo.ScheduleEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(),
                        InstructorId = c.Int(),
                        RoomId = c.Int(),
                        DayId = c.Int(nullable: false),
                        Period = c.Byte(nullable: false),
                        IsLecture = c.Boolean(nullable: false),
                        IsLab = c.Boolean(nullable: false),
                        IsTutor = c.Boolean(nullable: false),
                        LabGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .ForeignKey("dbo.Instructors", t => t.InstructorId)
                .ForeignKey("dbo.LabGroups", t => t.LabGroupId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.CourseId)
                .Index(t => t.InstructorId)
                .Index(t => t.RoomId)
                .Index(t => t.DayId)
                .Index(t => t.LabGroupId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        FirstName = c.String(),
                        FatherName = c.String(),
                        GrandFatherName = c.String(),
                        AccountId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.LabAssistants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignedLabRoomId = c.Int(),
                        FirstName = c.String(),
                        FatherName = c.String(),
                        GrandFatherName = c.String(),
                        AccountId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.AssignedLabRoomId)
                .Index(t => t.AssignedLabRoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LabAssistants", "AssignedLabRoomId", "dbo.Rooms");
            DropForeignKey("dbo.CourseOfferings", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.CourseOfferings", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "CurriculumId", "dbo.Curricula");
            DropForeignKey("dbo.Curricula", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Students", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Schedules", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Days", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.ScheduleEntries", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ScheduleEntries", "LabGroupId", "dbo.LabGroups");
            DropForeignKey("dbo.ScheduleEntries", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.ScheduleEntries", "DayId", "dbo.Days");
            DropForeignKey("dbo.ScheduleEntries", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Schedules", "AcademicSemesterId", "dbo.AcademicSemesters");
            DropForeignKey("dbo.Sections", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Sections", "AssignedLabRoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Sections", "AssignedLectureRoomId", "dbo.Rooms");
            DropForeignKey("dbo.LabGroups", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.LabGroups", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Departments", "DepartmentHeadId", "dbo.Instructors");
            DropForeignKey("dbo.InstructorPreferences", "Id", "dbo.Instructors");
            DropForeignKey("dbo.Instructors", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CourseOfferings", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.CourseOfferings", "AcademicSemesterId", "dbo.AcademicSemesters");
            DropForeignKey("dbo.AcademicSemesters", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.LabAssistants", new[] { "AssignedLabRoomId" });
            DropIndex("dbo.Students", new[] { "SectionId" });
            DropIndex("dbo.ScheduleEntries", new[] { "LabGroupId" });
            DropIndex("dbo.ScheduleEntries", new[] { "DayId" });
            DropIndex("dbo.ScheduleEntries", new[] { "RoomId" });
            DropIndex("dbo.ScheduleEntries", new[] { "InstructorId" });
            DropIndex("dbo.ScheduleEntries", new[] { "CourseId" });
            DropIndex("dbo.Days", new[] { "ScheduleId" });
            DropIndex("dbo.Schedules", new[] { "AcademicSemesterId" });
            DropIndex("dbo.Schedules", new[] { "SectionId" });
            DropIndex("dbo.LabGroups", new[] { "RoomId" });
            DropIndex("dbo.LabGroups", new[] { "SectionId" });
            DropIndex("dbo.Rooms", new[] { "BuildingId" });
            DropIndex("dbo.Sections", new[] { "AssignedLabRoomId" });
            DropIndex("dbo.Sections", new[] { "AssignedLectureRoomId" });
            DropIndex("dbo.Sections", new[] { "DepartmentId" });
            DropIndex("dbo.InstructorPreferences", new[] { "Id" });
            DropIndex("dbo.Instructors", new[] { "DepartmentId" });
            DropIndex("dbo.Departments", new[] { "DepartmentHeadId" });
            DropIndex("dbo.Departments", new[] { "FacultyId" });
            DropIndex("dbo.Curricula", new[] { "DepartmentId" });
            DropIndex("dbo.Courses", new[] { "CurriculumId" });
            DropIndex("dbo.CourseOfferings", new[] { "AcademicSemesterId" });
            DropIndex("dbo.CourseOfferings", new[] { "CourseId" });
            DropIndex("dbo.CourseOfferings", new[] { "InstructorId" });
            DropIndex("dbo.CourseOfferings", new[] { "SectionId" });
            DropIndex("dbo.AcademicSemesters", new[] { "AcademicYearId" });
            DropTable("dbo.LabAssistants");
            DropTable("dbo.Students");
            DropTable("dbo.ScheduleEntries");
            DropTable("dbo.Days");
            DropTable("dbo.Schedules");
            DropTable("dbo.Buildings");
            DropTable("dbo.LabGroups");
            DropTable("dbo.Rooms");
            DropTable("dbo.Sections");
            DropTable("dbo.Faculties");
            DropTable("dbo.InstructorPreferences");
            DropTable("dbo.Instructors");
            DropTable("dbo.Departments");
            DropTable("dbo.Curricula");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseOfferings");
            DropTable("dbo.AcademicYears");
            DropTable("dbo.AcademicSemesters");
            DropTable("dbo.AcademicEvents");
        }
    }
}
