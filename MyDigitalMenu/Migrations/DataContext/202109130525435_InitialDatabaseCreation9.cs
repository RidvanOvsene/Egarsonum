namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "CreatedDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.Orders", "UpdataDate", c => c.DateTime(precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "UpdataDate");
            DropColumn("dbo.Orders", "CreatedDate");
            DropColumn("dbo.Orders", "CategoryId");
        }
    }
}
