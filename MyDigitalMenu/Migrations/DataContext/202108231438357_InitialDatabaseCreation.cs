namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(unicode: false),
                        CategorySortOrder = c.Int(nullable: false),
                        CategoryDateAded = c.DateTime(nullable: false, precision: 0),
                        CategoryDateModified = c.DateTime(nullable: false, precision: 0),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        FoodId = c.Int(nullable: false, identity: true),
                        FoodName = c.String(unicode: false),
                        FoodPrice = c.Double(nullable: false),
                        FoodImageUrl = c.String(unicode: false),
                        FoodSortOrder = c.Int(nullable: false),
                        FoodIngredients1 = c.String(unicode: false),
                        FoodIngredients2 = c.String(unicode: false),
                        FoodIngredients3 = c.String(unicode: false),
                        FoodDescription = c.String(unicode: false),
                        FoodDateAded = c.DateTime(nullable: false, precision: 0),
                        FoodDateModified = c.DateTime(nullable: false, precision: 0),
                        CategoryId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FoodId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Password = c.String(maxLength: 500, storeType: "nvarchar"),
                        Email = c.String(maxLength: 500, storeType: "nvarchar"),
                        UserName = c.String(maxLength: 500, storeType: "nvarchar"),
                        CompanyName = c.String(maxLength: 500, storeType: "nvarchar"),
                        Role = c.String(maxLength: 500, storeType: "nvarchar"),
                        ProfileImgPath = c.String(maxLength: 150, storeType: "nvarchar"),
                        CreateDate = c.DateTime(precision: 0),
                        ModifiedDate = c.DateTime(precision: 0),
                        CreateedBy = c.String(maxLength: 50, storeType: "nvarchar"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Foods");
            DropTable("dbo.Categories");
        }
    }
}
