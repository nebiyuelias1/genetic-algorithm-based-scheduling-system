namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChangeIntroduced : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Instructors", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Instructors", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
