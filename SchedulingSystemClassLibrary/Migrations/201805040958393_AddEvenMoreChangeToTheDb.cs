namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEvenMoreChangeToTheDb : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Instructors", "InstructorPreferenceId", c => c.Int());
            AddColumn("dbo.Instructors", "AccountId", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InstructorPreferences", "Id", "dbo.Instructors");
            DropIndex("dbo.InstructorPreferences", new[] { "Id" });
            DropColumn("dbo.Instructors", "AccountId");
            DropColumn("dbo.Instructors", "InstructorPreferenceId");
            DropTable("dbo.InstructorPreferences");
        }
    }
}
