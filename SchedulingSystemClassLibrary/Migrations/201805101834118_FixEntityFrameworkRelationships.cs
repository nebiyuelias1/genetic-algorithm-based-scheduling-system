namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixEntityFrameworkRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LabAssistances", "RoomId", "dbo.Rooms");
            DropIndex("dbo.LabAssistances", new[] { "RoomId" });
            //DropColumn("dbo.Rooms", "LabAssistanceId");
            DropColumn("dbo.LabAssistances", "RoomId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LabAssistances", "RoomId", c => c.Int());
            //AddColumn("dbo.Rooms", "LabAssistanceId", c => c.Int(nullable: false));
            CreateIndex("dbo.LabAssistances", "RoomId");
            AddForeignKey("dbo.LabAssistances", "RoomId", "dbo.Rooms", "Id");
        }
    }
}
