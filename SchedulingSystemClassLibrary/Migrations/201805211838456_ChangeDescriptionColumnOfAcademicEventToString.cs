namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDescriptionColumnOfAcademicEventToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AcademicEvents", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AcademicEvents", "Description", c => c.Int(nullable: false));
        }
    }
}
