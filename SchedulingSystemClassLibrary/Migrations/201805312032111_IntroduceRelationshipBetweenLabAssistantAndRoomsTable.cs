namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntroduceRelationshipBetweenLabAssistantAndRoomsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LabAssistants", "AssignedLabRoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.LabAssistants", "AssignedLabRoomId");
            AddForeignKey("dbo.LabAssistants", "AssignedLabRoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LabAssistants", "AssignedLabRoomId", "dbo.Rooms");
            DropIndex("dbo.LabAssistants", new[] { "AssignedLabRoomId" });
            DropColumn("dbo.LabAssistants", "AssignedLabRoomId");
        }
    }
}
