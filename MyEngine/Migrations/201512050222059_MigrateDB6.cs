namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "RegionId", "dbo.Regions");
            DropIndex("dbo.Cities", new[] { "RegionId" });
            DropTable("dbo.Cities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Cities", "RegionId");
            AddForeignKey("dbo.Cities", "RegionId", "dbo.Regions", "Id");
        }
    }
}
