namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topkek : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "IsSelled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Publications", "IsApprovedByAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Publications", "AmountOfPageViews", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "AmountOfPageViews");
            DropColumn("dbo.Publications", "IsApprovedByAdmin");
            DropColumn("dbo.Publications", "IsSelled");
        }
    }
}
