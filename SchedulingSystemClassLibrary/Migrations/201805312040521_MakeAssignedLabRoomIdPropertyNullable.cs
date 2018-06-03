namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeAssignedLabRoomIdPropertyNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LabAssistants", "AssignedLabRoomId", "dbo.Rooms");
            DropIndex("dbo.LabAssistants", new[] { "AssignedLabRoomId" });
            AlterColumn("dbo.LabAssistants", "AssignedLabRoomId", c => c.Int());
            CreateIndex("dbo.LabAssistants", "AssignedLabRoomId");
            AddForeignKey("dbo.LabAssistants", "AssignedLabRoomId", "dbo.Rooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LabAssistants", "AssignedLabRoomId", "dbo.Rooms");
            DropIndex("dbo.LabAssistants", new[] { "AssignedLabRoomId" });
            AlterColumn("dbo.LabAssistants", "AssignedLabRoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.LabAssistants", "AssignedLabRoomId");
            AddForeignKey("dbo.LabAssistants", "AssignedLabRoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
    }
}
