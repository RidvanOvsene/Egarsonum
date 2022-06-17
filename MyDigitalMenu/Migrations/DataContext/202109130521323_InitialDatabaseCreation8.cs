namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        TableId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                        FoodName = c.String(unicode: false),
                        FoodCount = c.Int(nullable: false),
                        UserId = c.String(unicode: false),
                        Price = c.String(unicode: false),
                        RestorantId = c.String(unicode: false),
                        State = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
