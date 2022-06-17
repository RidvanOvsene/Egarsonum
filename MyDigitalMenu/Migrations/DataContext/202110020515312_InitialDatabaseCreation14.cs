namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation14 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderPaids");
            AddColumn("dbo.OrderPaids", "OrderPaidId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrderPaids", "OrderPaidId");
            DropColumn("dbo.OrderPaids", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderPaids", "OrderId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.OrderPaids");
            DropColumn("dbo.OrderPaids", "OrderPaidId");
            AddPrimaryKey("dbo.OrderPaids", "OrderId");
        }
    }
}
