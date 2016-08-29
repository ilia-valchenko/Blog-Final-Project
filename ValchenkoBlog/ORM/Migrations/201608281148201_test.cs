namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Nickname" });
            DropIndex("dbo.Roles", new[] { "Name" });
            AlterColumn("dbo.Users", "Nickname", c => c.String(nullable: false));
            AlterColumn("dbo.Roles", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "Nickname", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.Roles", "Name", unique: true);
            CreateIndex("dbo.Users", "Nickname", unique: true);
        }
    }
}
