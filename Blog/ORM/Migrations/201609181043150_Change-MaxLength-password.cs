namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMaxLengthpassword : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
