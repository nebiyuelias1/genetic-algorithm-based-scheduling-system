namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChangeDetected : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LabAssistances", newName: "LabAssistants");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.LabAssistants", newName: "LabAssistances");
        }
    }
}
