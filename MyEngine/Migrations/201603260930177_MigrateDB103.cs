namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB103 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "LinkName");
            DropColumn("dbo.Categories", "SectionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "SectionId", c => c.Int());
            AddColumn("dbo.Categories", "LinkName", c => c.String());
        }
    }
}
