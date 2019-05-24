namespace VidlyExercice1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6c29c51f-1d74-4542-86e1-0fedc54142ed', N'guest@vidly.com', 0, N'ACacXAxtCjZqNFo+tlU7L2Wg2r5boJ1Jr4YQyt3KbZUosjYs6mi7w0s6w/da2Ylllg==', N'8bf31afc-93ef-4897-bb5b-dadecf4a5c24', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'93588659-b4bd-48b2-b8d8-c0a6639633cb', N'admin@vidly.com', 0, N'AMbT/AI2o8BFBYW9pWjiI3qDGPgdVhN3J5NUdHHItwGwhfFFrUpRTpXmpCplwSgGmw==', N'7a7a910c-dcaa-47d1-88e3-60f51d40bc12', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9e2bd658-cce0-4fa2-a715-89af1f66d593', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'93588659-b4bd-48b2-b8d8-c0a6639633cb', N'9e2bd658-cce0-4fa2-a715-89af1f66d593')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
