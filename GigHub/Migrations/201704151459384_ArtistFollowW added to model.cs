namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtistFollowWaddedtomodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowArtists",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        ArtistdId = c.String(nullable: false, maxLength: 128),
                        Artist_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.ArtistdId })
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId, cascadeDelete: true)
                .Index(t => t.FollowerId)
                .Index(t => t.Artist_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowArtists", "FollowerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowArtists", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FollowArtists", new[] { "Artist_Id" });
            DropIndex("dbo.FollowArtists", new[] { "FollowerId" });
            DropTable("dbo.FollowArtists");
        }
    }
}
