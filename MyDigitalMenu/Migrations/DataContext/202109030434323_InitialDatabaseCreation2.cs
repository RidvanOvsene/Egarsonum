namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "RestorantId", c => c.String(unicode: false));
            AlterColumn("dbo.Foods", "RestorantId", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "RestorantId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Categories", "RestorantId", c => c.Guid(nullable: false));
        }
    }
}
