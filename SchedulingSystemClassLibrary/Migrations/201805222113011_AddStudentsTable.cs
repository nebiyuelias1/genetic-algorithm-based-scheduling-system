namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        FirstName = c.String(),
                        FatherName = c.String(),
                        GrandFatherName = c.String(),
                        AccountId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.SectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "SectionId", "dbo.Sections");
            DropIndex("dbo.Students", new[] { "SectionId" });
            DropTable("dbo.Students");
        }
    }
}
