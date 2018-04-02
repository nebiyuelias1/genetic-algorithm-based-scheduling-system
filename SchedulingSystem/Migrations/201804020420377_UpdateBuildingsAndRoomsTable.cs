namespace SchedulingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBuildingsAndRoomsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RoomSections", newName: "SectionRooms");
            DropPrimaryKey("dbo.SectionRooms");
            AddColumn("dbo.Rooms", "BuildingId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SectionRooms", new[] { "Section_Id", "Room_Id" });
            CreateIndex("dbo.Rooms", "BuildingId");
            AddForeignKey("dbo.Rooms", "BuildingId", "dbo.Buildings", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "BuildingId", "dbo.Buildings");
            DropIndex("dbo.Rooms", new[] { "BuildingId" });
            DropPrimaryKey("dbo.SectionRooms");
            DropColumn("dbo.Rooms", "BuildingId");
            AddPrimaryKey("dbo.SectionRooms", new[] { "Room_Id", "Section_Id" });
            RenameTable(name: "dbo.SectionRooms", newName: "RoomSections");
        }
    }
}
