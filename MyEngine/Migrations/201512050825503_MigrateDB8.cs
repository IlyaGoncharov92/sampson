namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Towns", "RegionId", c => c.Int());
            CreateIndex("dbo.Towns", "RegionId");
            AddForeignKey("dbo.Towns", "RegionId", "dbo.Regions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Towns", "RegionId", "dbo.Regions");
            DropIndex("dbo.Towns", new[] { "RegionId" });
            DropColumn("dbo.Towns", "RegionId");
        }
    }
}
