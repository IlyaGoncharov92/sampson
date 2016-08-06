namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Declarations", "RegionId", c => c.Int());
            CreateIndex("dbo.Declarations", "RegionId");
            AddForeignKey("dbo.Declarations", "RegionId", "dbo.Regions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Declarations", "RegionId", "dbo.Regions");
            DropIndex("dbo.Declarations", new[] { "RegionId" });
            DropColumn("dbo.Declarations", "RegionId");
        }
    }
}
