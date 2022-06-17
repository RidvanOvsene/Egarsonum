namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DinnerTables", "State", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DinnerTables", "State");
        }
    }
}
