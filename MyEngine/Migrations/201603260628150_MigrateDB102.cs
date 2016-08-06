namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB102 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Declarations", "PhoneNumber");
            DropColumn("dbo.Declarations", "StrCoast");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Declarations", "StrCoast", c => c.String());
            AddColumn("dbo.Declarations", "PhoneNumber", c => c.String());
        }
    }
}
