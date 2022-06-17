namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation1 : DbMigration
    {
        public override void Up()
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
            
            AddColumn("dbo.Categories", "RestorantId", c => c.Guid(nullable: false));
            AddColumn("dbo.Foods", "RestorantId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Foods", "RestorantId");
            DropColumn("dbo.Categories", "RestorantId");
            DropTable("dbo.FoodItems");
        }
    }
}
