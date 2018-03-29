namespace SchedulingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInstructorsPreferenceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InstructorPreferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstructorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructors", t => t.InstructorId)
                .Index(t => t.InstructorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InstructorPreferences", "InstructorId", "dbo.Instructors");
            DropIndex("dbo.InstructorPreferences", new[] { "InstructorId" });
            DropTable("dbo.InstructorPreferences");
        }
    }
}
