namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAcademicEventsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        Color = c.Int(nullable: false),
                        IsFullDay = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AcademicEvents");
        }
    }
}
