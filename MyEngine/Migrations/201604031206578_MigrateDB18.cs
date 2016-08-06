namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Declarations", "Article", c => c.Int());
            AddColumn("dbo.Declarations", "Color", c => c.Int());
            AddColumn("dbo.Declarations", "Сonsist", c => c.Int());
            AddColumn("dbo.Declarations", "Size", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Declarations", "Size");
            DropColumn("dbo.Declarations", "Сonsist");
            DropColumn("dbo.Declarations", "Color");
            DropColumn("dbo.Declarations", "Article");
        }
    }
}
