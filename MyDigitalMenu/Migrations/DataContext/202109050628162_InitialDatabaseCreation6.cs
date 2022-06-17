namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Foods", "FoodIngredients1");
            DropColumn("dbo.Foods", "FoodIngredients2");
            DropColumn("dbo.Foods", "FoodIngredients3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "FoodIngredients3", c => c.String(unicode: false));
            AddColumn("dbo.Foods", "FoodIngredients2", c => c.String(unicode: false));
            AddColumn("dbo.Foods", "FoodIngredients1", c => c.String(unicode: false));
        }
    }
}
