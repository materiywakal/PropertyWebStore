namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Publications", "PublicName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Publications", "PublicName", c => c.String());
            DropColumn("dbo.Publications", "IsActive");
        }
    }
}
