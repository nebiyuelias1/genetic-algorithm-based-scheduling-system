namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccountIdPropertyToInstructorsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "AccountId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructors", "AccountId");
        }
    }
}
