namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BalconyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BathroomTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlockOfFlatsTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User1Id = c.Int(nullable: false),
                        User2Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User1Id, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User2Id, cascadeDelete: false)
                .Index(t => t.User1Id)
                .Index(t => t.User2Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ChatId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ChatId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConnectionId = c.String(),
                        Name = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        ConfirmedEmail = c.Boolean(nullable: false),
                        Phone = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PropertyTypeId = c.Int(nullable: false),
                        IsRent = c.Boolean(nullable: false),
                        Floor = c.Int(nullable: false),
                        RoomAmount = c.Int(nullable: false),
                        OfferingRoomAmount = c.Int(nullable: false),
                        IsPassageRoom = c.Boolean(nullable: false),
                        IsFurnitureExist = c.Boolean(nullable: false),
                        TotalArea = c.Single(nullable: false),
                        LivingArea = c.Single(nullable: false),
                        KitchenArea = c.Single(nullable: false),
                        PropertyArea = c.Single(nullable: false),
                        BlockOfFlatsTypeId = c.Int(nullable: false),
                        BathroomTypeId = c.Int(nullable: false),
                        BalconyTypeId = c.Int(nullable: false),
                        YearOfConstruction = c.Int(nullable: false),
                        WallMaterialId = c.Int(nullable: false),
                        Description = c.String(),
                        PostTime = c.DateTime(nullable: false),
                        Cost = c.Single(nullable: false),
                        Address = c.String(),
                        Coordinates = c.String(),
                        IsOneDayRent = c.Boolean(nullable: false),
                        CanExchange = c.Boolean(nullable: false),
                        IsSelled = c.Boolean(nullable: false),
                        IsApprovedByAdmin = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        AmountOfPageViews = c.Int(nullable: false),
                        IsSubwayNear = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BalconyTypes", t => t.BalconyTypeId, cascadeDelete: true)
                .ForeignKey("dbo.BathroomTypes", t => t.BathroomTypeId, cascadeDelete: true)
                .ForeignKey("dbo.BlockOfFlatsTypes", t => t.BlockOfFlatsTypeId, cascadeDelete: true)
                .ForeignKey("dbo.PropertyTypes", t => t.PropertyTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.WallMaterials", t => t.WallMaterialId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PropertyTypeId)
                .Index(t => t.BlockOfFlatsTypeId)
                .Index(t => t.BathroomTypeId)
                .Index(t => t.BalconyTypeId)
                .Index(t => t.WallMaterialId);
            
            CreateTable(
                "dbo.PropertyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WallMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chats", "User2Id", "dbo.Users");
            DropForeignKey("dbo.Chats", "User1Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Publications", "WallMaterialId", "dbo.WallMaterials");
            DropForeignKey("dbo.Publications", "UserId", "dbo.Users");
            DropForeignKey("dbo.Publications", "PropertyTypeId", "dbo.PropertyTypes");
            DropForeignKey("dbo.Publications", "BlockOfFlatsTypeId", "dbo.BlockOfFlatsTypes");
            DropForeignKey("dbo.Publications", "BathroomTypeId", "dbo.BathroomTypes");
            DropForeignKey("dbo.Publications", "BalconyTypeId", "dbo.BalconyTypes");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropIndex("dbo.Publications", new[] { "WallMaterialId" });
            DropIndex("dbo.Publications", new[] { "BalconyTypeId" });
            DropIndex("dbo.Publications", new[] { "BathroomTypeId" });
            DropIndex("dbo.Publications", new[] { "BlockOfFlatsTypeId" });
            DropIndex("dbo.Publications", new[] { "PropertyTypeId" });
            DropIndex("dbo.Publications", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Chats", new[] { "User2Id" });
            DropIndex("dbo.Chats", new[] { "User1Id" });
            DropTable("dbo.Roles");
            DropTable("dbo.WallMaterials");
            DropTable("dbo.PropertyTypes");
            DropTable("dbo.Publications");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.Chats");
            DropTable("dbo.BlockOfFlatsTypes");
            DropTable("dbo.BathroomTypes");
            DropTable("dbo.BalconyTypes");
        }
    }
}
