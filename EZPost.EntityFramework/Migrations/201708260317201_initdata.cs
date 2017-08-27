namespace EZPost.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class initdata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                        GameDesc = c.String(),
                        GamePic = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Content = c.String(),
                        OrderStatus = c.Int(nullable: false),
                        ProductId = c.Int(),
                        OrderUserId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionTime = c.DateTime(),
                        DeleterUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Order_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderUsers", t => t.OrderUserId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.OrderUserId);
            
            CreateTable(
                "dbo.OrderUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Phone = c.String(),
                        AccountName = c.String(),
                        Password = c.String(),
                        ServerName = c.String(),
                        CharacterName = c.String(),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Time = c.String(),
                        Grade = c.String(),
                        Decs = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OriginPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PicUrl = c.String(),
                        LevelingType = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "GameId", "dbo.Games");
            DropForeignKey("dbo.Orders", "OrderUserId", "dbo.OrderUsers");
            DropIndex("dbo.Products", new[] { "GameId" });
            DropIndex("dbo.Orders", new[] { "OrderUserId" });
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropTable("dbo.Products");
            DropTable("dbo.OrderUsers");
            DropTable("dbo.Orders",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Order_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Games");
        }
    }
}
