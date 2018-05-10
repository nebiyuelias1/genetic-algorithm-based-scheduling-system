namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedDatabase : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f6dadcb4-7264-4e46-995e-0a63a35eefa2', N'IsACollegeDean')
                    INSERT INTO[dbo].[AspNetRoles]([Id], [Name]) VALUES(N'7901a1e3-da96-4b1f-81b3-f0bfeac479bc', N'IsADepartmentHead')
                    INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'2c3910bb-6111-4caa-922f-dc7ec1fbb85f', N'IsALabAssistant')
                    INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'd2d6eea8-aaaa-448e-a798-c6e930546c91', N'IsAnInstructor')
                    INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'84462b06-2434-465d-b6a9-163441bb7013', N'IsAStudent')
                ");

        }
        
        public override void Down()
        {
        }
    }
}
