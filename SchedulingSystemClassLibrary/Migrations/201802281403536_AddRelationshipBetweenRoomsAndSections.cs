namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationshipBetweenRoomsAndSections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SectionRooms",
                c => new
                    {
                        Section_Id = c.Int(nullable: false),
                        Room_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Section_Id, t.Room_Id })
                .ForeignKey("dbo.Sections", t => t.Section_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .Index(t => t.Section_Id)
                .Index(t => t.Room_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SectionRooms", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.SectionRooms", "Section_Id", "dbo.Sections");
            DropIndex("dbo.SectionRooms", new[] { "Room_Id" });
            DropIndex("dbo.SectionRooms", new[] { "Section_Id" });
            DropTable("dbo.SectionRooms");
        }
    }
}
