namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "State", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Orders", "Price", c => c.String(unicode: false));
        }
    }
}
