namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Declarations", "RegionId", "dbo.Regions");
            DropIndex("dbo.Declarations", new[] { "RegionId" });
            DropColumn("dbo.Declarations", "RegionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Declarations", "RegionId", c => c.Int());
            CreateIndex("dbo.Declarations", "RegionId");
            AddForeignKey("dbo.Declarations", "RegionId", "dbo.Regions", "Id");
        }
    }
}
