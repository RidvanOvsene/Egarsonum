namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation3 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.FoodItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FoodItems",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(unicode: false),
                        Text = c.String(unicode: false),
                        CategoryDateAded = c.DateTime(nullable: false, precision: 0),
                        CategoryDateModified = c.DateTime(nullable: false, precision: 0),
                        UserId = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
        }
    }
}
