namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepartmentHeadToDepartmentsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "DepartmentHeadId", c => c.Int());
            AddColumn("dbo.Instructors", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Departments", "DepartmentHeadId");
            AddForeignKey("dbo.Departments", "DepartmentHeadId", "dbo.Instructors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "DepartmentHeadId", "dbo.Instructors");
            DropIndex("dbo.Departments", new[] { "DepartmentHeadId" });
            DropColumn("dbo.Instructors", "Discriminator");
            DropColumn("dbo.Departments", "DepartmentHeadId");
        }
    }
}
