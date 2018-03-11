namespace SchedulingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyInstructorsAndDepartmentsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Instructors", "DepartmentId");
            AddForeignKey("dbo.Instructors", "DepartmentId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instructors", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Instructors", new[] { "DepartmentId" });
            DropColumn("dbo.Instructors", "DepartmentId");
        }
    }
}
