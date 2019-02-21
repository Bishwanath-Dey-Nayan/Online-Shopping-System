namespace NayanTraders.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Contact = c.String(),
                        GenderId = c.Int(nullable: false),
                        Address = c.String(),
                        CountryId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Email = c.String(),
                        UserTypeId = c.Int(nullable: false),
                        PassWord = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Genders", t => t.GenderId)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId)
                .Index(t => t.GenderId)
                .Index(t => t.CountryId)
                .Index(t => t.CityId)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleCode = c.String(),
                        Date = c.DateTime(nullable: false),
                        AccountId = c.Int(nullable: false),
                        Total = c.String(),
                        Accounts_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Accounts_id)
                .Index(t => t.Accounts_id);
            
            CreateTable(
                "dbo.SalesDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        rate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.SaleId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        CategoryId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                        UnitId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        Price = c.String(),
                        Discount = c.String(),
                        Image = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Sizes", t => t.SizeId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .Index(t => t.CategoryId)
                .Index(t => t.SizeId)
                .Index(t => t.UnitId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        Rate = c.Single(nullable: false),
                        Amount = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId)
                .Index(t => t.PurchaseId)
                .Index(t => t.ProductId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseCode = c.String(),
                        PurchaseDate = c.DateTime(nullable: false),
                        VendorId = c.Int(nullable: false),
                        total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendors", t => t.VendorId)
                .Index(t => t.VendorId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Branch = c.String(),
                        Contact = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.SalesDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Products", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Products", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.SalesDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Purchases", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.PurchaseDetails", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.PurchaseDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchaseDetails", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Sales", "Accounts_id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Accounts", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Accounts", "CityId", "dbo.Cities");
            DropIndex("dbo.Purchases", new[] { "VendorId" });
            DropIndex("dbo.PurchaseDetails", new[] { "BrandId" });
            DropIndex("dbo.PurchaseDetails", new[] { "ProductId" });
            DropIndex("dbo.PurchaseDetails", new[] { "PurchaseId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "UnitId" });
            DropIndex("dbo.Products", new[] { "SizeId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.SalesDetails", new[] { "ProductId" });
            DropIndex("dbo.SalesDetails", new[] { "SaleId" });
            DropIndex("dbo.Sales", new[] { "Accounts_id" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Accounts", new[] { "UserTypeId" });
            DropIndex("dbo.Accounts", new[] { "CityId" });
            DropIndex("dbo.Accounts", new[] { "CountryId" });
            DropIndex("dbo.Accounts", new[] { "GenderId" });
            DropTable("dbo.UserTypes");
            DropTable("dbo.Units");
            DropTable("dbo.Sizes");
            DropTable("dbo.Categories");
            DropTable("dbo.Vendors");
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchaseDetails");
            DropTable("dbo.Brands");
            DropTable("dbo.Products");
            DropTable("dbo.SalesDetails");
            DropTable("dbo.Sales");
            DropTable("dbo.Genders");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Accounts");
        }
    }
}
