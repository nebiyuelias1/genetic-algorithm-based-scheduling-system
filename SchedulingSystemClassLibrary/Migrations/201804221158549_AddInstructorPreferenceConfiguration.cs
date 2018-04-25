namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInstructorPreferenceConfiguration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InstructorPreferences", "PreferenceData", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InstructorPreferences", "PreferenceData");
        }
    }
}
