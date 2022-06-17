namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItems", "FoodId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodItems", "FoodId");
        }
    }
}
