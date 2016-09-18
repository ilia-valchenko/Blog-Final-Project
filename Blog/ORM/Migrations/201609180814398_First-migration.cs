namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 300),
                        PublishDate = c.DateTime(nullable: false),
                        Post_PostId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Post_PostId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        Post_PostId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.LikeId)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Post_PostId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Nickname = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 30),
                        Avatar = c.Binary(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_RoleId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleId, t.User_UserId })
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Role_RoleId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Post_PostId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_PostId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Post_PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPosts", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.RoleUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.Posts", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Likes", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Likes", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.TagPosts", new[] { "Post_PostId" });
            DropIndex("dbo.TagPosts", new[] { "Tag_TagId" });
            DropIndex("dbo.RoleUsers", new[] { "User_UserId" });
            DropIndex("dbo.RoleUsers", new[] { "Role_RoleId" });
            DropIndex("dbo.Likes", new[] { "User_UserId" });
            DropIndex("dbo.Likes", new[] { "Post_PostId" });
            DropIndex("dbo.Posts", new[] { "User_UserId" });
            DropIndex("dbo.Comments", new[] { "User_UserId" });
            DropIndex("dbo.Comments", new[] { "Post_PostId" });
            DropTable("dbo.TagPosts");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Tags");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Likes");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
