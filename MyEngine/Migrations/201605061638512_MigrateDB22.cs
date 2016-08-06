namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IdTitle", c => c.String());
            AddColumn("dbo.Sections", "IdTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "IdTitle");
            DropColumn("dbo.Categories", "IdTitle");
        }
    }
}
