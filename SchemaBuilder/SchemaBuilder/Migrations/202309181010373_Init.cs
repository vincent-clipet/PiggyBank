namespace SchemaBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        ManufacturerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ManufacturerId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Status_OrderStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.OrderStatus", t => t.Status_OrderStatusId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AddressId)
                .Index(t => t.Status_OrderStatusId);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        OrderStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.OrderStatusId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        Description = c.String(),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        Color = c.String(),
                        Price = c.Int(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.ReviewStatus",
                c => new
                    {
                        ReviewStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ReviewStatusId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        Message = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Status_ReviewStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.ReviewStatus", t => t.Status_ReviewStatusId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Status_ReviewStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Status_ReviewStatusId", "dbo.ReviewStatus");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Orders", "Status_OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Manufacturers", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Reviews", new[] { "Status_ReviewStatusId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Orders", new[] { "Status_OrderStatusId" });
            DropIndex("dbo.Orders", new[] { "AddressId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Manufacturers", new[] { "AddressId" });
            DropTable("dbo.Reviews");
            DropTable("dbo.ReviewStatus");
            DropTable("dbo.Products");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Addresses");
        }
    }
}
