namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderPaids",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        TableId = c.Int(nullable: false),
                        UserId = c.String(unicode: false),
                        TotalPrice = c.Double(nullable: false),
                        RestorantId = c.String(unicode: false),
                        CreatedDate = c.DateTime(precision: 0),
                        UpdataDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderPaids");
        }
    }
}
