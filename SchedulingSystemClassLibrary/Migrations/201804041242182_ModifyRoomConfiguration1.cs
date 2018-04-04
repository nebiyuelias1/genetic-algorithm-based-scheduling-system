namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyRoomConfiguration1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sections", new[] { "AssignedLectureRoomId" });
            DropIndex("dbo.Sections", new[] { "AssignedLabRoomId" });
            AlterColumn("dbo.Sections", "AssignedLectureRoomId", c => c.Int());
            AlterColumn("dbo.Sections", "AssignedLabRoomId", c => c.Int());
            CreateIndex("dbo.Sections", "AssignedLectureRoomId");
            CreateIndex("dbo.Sections", "AssignedLabRoomId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sections", new[] { "AssignedLabRoomId" });
            DropIndex("dbo.Sections", new[] { "AssignedLectureRoomId" });
            AlterColumn("dbo.Sections", "AssignedLabRoomId", c => c.Int(nullable: false));
            AlterColumn("dbo.Sections", "AssignedLectureRoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sections", "AssignedLabRoomId");
            CreateIndex("dbo.Sections", "AssignedLectureRoomId");
        }
    }
}
