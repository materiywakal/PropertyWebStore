namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class probablyFinal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "IsSubwayNear", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "IsSubwayNear");
        }
    }
}
