namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeArtisFollowtfollowings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FollowArtists", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowArtists", "FollowerId", "dbo.AspNetUsers");
            DropIndex("dbo.FollowArtists", new[] { "FollowerId" });
            DropIndex("dbo.FollowArtists", new[] { "Artist_Id" });
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        FolloweeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FolloweeId })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .ForeignKey("dbo.AspNetUsers", t => t.FolloweeId)
                .Index(t => t.FollowerId)
                .Index(t => t.FolloweeId);
            
            DropTable("dbo.FollowArtists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FollowArtists",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        ArtistdId = c.String(nullable: false, maxLength: 128),
                        Artist_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.ArtistdId });
            
            DropForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "FolloweeId" });
            DropIndex("dbo.Followings", new[] { "FollowerId" });
            DropTable("dbo.Followings");
            CreateIndex("dbo.FollowArtists", "Artist_Id");
            CreateIndex("dbo.FollowArtists", "FollowerId");
            AddForeignKey("dbo.FollowArtists", "FollowerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FollowArtists", "Artist_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
