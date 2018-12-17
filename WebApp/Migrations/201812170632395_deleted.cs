namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "IsDeleted");
        }
    }
}
