namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAcademicYearsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AcademicYears", "GcYear", c => c.String());
            AlterColumn("dbo.AcademicYears", "EtYear", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AcademicYears", "EtYear", c => c.Int(nullable: false));
            AlterColumn("dbo.AcademicYears", "GcYear", c => c.Int(nullable: false));
        }
    }
}
