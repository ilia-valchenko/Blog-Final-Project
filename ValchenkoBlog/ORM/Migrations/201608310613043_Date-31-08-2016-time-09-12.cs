namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Date31082016time0912 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String());
            AlterColumn("dbo.Posts", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 300));
        }
    }
}
