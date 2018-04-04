namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyRoomConfiguration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomSections", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.RoomSections", "Section_Id", "dbo.Sections");
            DropIndex("dbo.RoomSections", new[] { "Room_Id" });
            DropIndex("dbo.RoomSections", new[] { "Section_Id" });
            AddColumn("dbo.Sections", "AssignedLectureRoomId", c => c.Int(nullable: false));
            AddColumn("dbo.Sections", "AssignedLabRoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sections", "AssignedLectureRoomId");
            CreateIndex("dbo.Sections", "AssignedLabRoomId");
            AddForeignKey("dbo.Sections", "AssignedLabRoomId", "dbo.Rooms", "Id");
            AddForeignKey("dbo.Sections", "AssignedLectureRoomId", "dbo.Rooms", "Id");
            DropTable("dbo.RoomSections");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoomSections",
                c => new
                    {
                        Room_Id = c.Int(nullable: false),
                        Section_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Room_Id, t.Section_Id });
            
            DropForeignKey("dbo.Sections", "AssignedLectureRoomId", "dbo.Rooms");
            DropForeignKey("dbo.Sections", "AssignedLabRoomId", "dbo.Rooms");
            DropIndex("dbo.Sections", new[] { "AssignedLabRoomId" });
            DropIndex("dbo.Sections", new[] { "AssignedLectureRoomId" });
            DropColumn("dbo.Sections", "AssignedLabRoomId");
            DropColumn("dbo.Sections", "AssignedLectureRoomId");
            CreateIndex("dbo.RoomSections", "Section_Id");
            CreateIndex("dbo.RoomSections", "Room_Id");
            AddForeignKey("dbo.RoomSections", "Section_Id", "dbo.Sections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoomSections", "Room_Id", "dbo.Rooms", "Id", cascadeDelete: true);
        }
    }
}
