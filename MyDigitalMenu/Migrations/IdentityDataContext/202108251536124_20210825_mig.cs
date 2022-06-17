namespace MyDigitalMenu.Migrations.IdentityDataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20210825_mig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CompanyName", c => c.String(unicode: false));
            AddColumn("dbo.AspNetUsers", "Adress", c => c.String(unicode: false));
            AddColumn("dbo.AspNetUsers", "Tel", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Tel");
            DropColumn("dbo.AspNetUsers", "Adress");
            DropColumn("dbo.AspNetUsers", "CompanyName");
        }
    }
}
