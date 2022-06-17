namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderPaids", "PaymentType", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderPaids", "PaymentType");
        }
    }
}
