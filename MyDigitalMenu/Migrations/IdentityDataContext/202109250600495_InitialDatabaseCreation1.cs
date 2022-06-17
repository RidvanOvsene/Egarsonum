namespace MyDigitalMenu.Migrations.IdentityDataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RestorantId", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RestorantId");
        }
    }
}
