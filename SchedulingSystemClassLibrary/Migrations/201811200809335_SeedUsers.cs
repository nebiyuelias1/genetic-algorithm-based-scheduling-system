namespace SchedulingSystemClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd208819c-4841-40d6-a241-dde57f988f3b', N'betselot@gmail.com', 1, N'ABgVsOhQdtKMmntIakc4Azh7zzhf8mtgst2cbAZ+0zRx9ewxF1P6LqOr2357wmT5rw==', N'cc2740fa-48e6-4c8e-a875-fc8f2f5616bf', NULL, 0, 0, NULL, 0, 0, N'betselot@gmail.com')
                    INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'ecc9c7cc-bc81-4591-af7d-86f8a66ec5d2', N'admin@dbu.edu.et', 0, N'ACFmliXHN4ai/CzmOEaN8tx4T+bre1g5K1Vuoq3DsdE0jZcidIaDXfvj2DYDf+EY6w==', N'6353c1ed-00ac-431f-b3d1-595bc76bb0f8', NULL, 0, 0, NULL, 1, 0, N'admin@dbu.edu.et')
                ");

            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd208819c-4841-40d6-a241-dde57f988f3b', N'7901a1e3-da96-4b1f-81b3-f0bfeac479bc')
                    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd208819c-4841-40d6-a241-dde57f988f3b', N'd2d6eea8-aaaa-448e-a798-c6e930546c91')
                    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ecc9c7cc-bc81-4591-af7d-86f8a66ec5d2', N'f6dadcb4-7264-4e46-995e-0a63a35eefa2')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
