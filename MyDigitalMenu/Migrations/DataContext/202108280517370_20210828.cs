namespace MyDigitalMenu.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20210828 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "UserId", c => c.String(unicode: false));
            AlterColumn("dbo.Foods", "UserId", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "UserId", c => c.Int(nullable: false));
        }
    }
}
