namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ImageOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Images", "ImageType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "ImageType");
            DropColumn("dbo.Images", "ImageOrder");
        }
    }
}
