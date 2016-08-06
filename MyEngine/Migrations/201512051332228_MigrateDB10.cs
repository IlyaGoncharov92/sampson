namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Declarations", "TownId", "dbo.Towns");
            DropIndex("dbo.Declarations", new[] { "TownId" });
            AlterColumn("dbo.Declarations", "TownId", c => c.Int(nullable: false));
            CreateIndex("dbo.Declarations", "TownId");
            AddForeignKey("dbo.Declarations", "TownId", "dbo.Towns", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Declarations", "TownId", "dbo.Towns");
            DropIndex("dbo.Declarations", new[] { "TownId" });
            AlterColumn("dbo.Declarations", "TownId", c => c.Int());
            CreateIndex("dbo.Declarations", "TownId");
            AddForeignKey("dbo.Declarations", "TownId", "dbo.Towns", "Id");
        }
    }
}
