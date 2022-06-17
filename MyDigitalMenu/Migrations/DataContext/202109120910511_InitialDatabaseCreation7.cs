namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DinnerTables",
                c => new
                    {
                        TableId = c.Int(nullable: false, identity: true),
                        TableName = c.String(unicode: false),
                        TableSortOrder = c.Int(nullable: false),
                        UserId = c.String(unicode: false),
                        RestorantId = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.TableId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DinnerTables");
        }
    }
}
