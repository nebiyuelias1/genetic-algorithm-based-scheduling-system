namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLabGroupsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LabGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        Name = c.String(),
                        RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LabGroups", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.LabGroups", "RoomId", "dbo.Rooms");
            DropIndex("dbo.LabGroups", new[] { "RoomId" });
            DropIndex("dbo.LabGroups", new[] { "SectionId" });
            DropTable("dbo.LabGroups");
        }
    }
}
