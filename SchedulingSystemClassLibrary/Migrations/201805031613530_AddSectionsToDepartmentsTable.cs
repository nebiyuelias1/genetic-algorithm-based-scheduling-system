namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSectionsToDepartmentsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sections", "DepartmentId", "dbo.Departments");
            AddForeignKey("dbo.Sections", "DepartmentId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "DepartmentId", "dbo.Departments");
            AddForeignKey("dbo.Sections", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}
