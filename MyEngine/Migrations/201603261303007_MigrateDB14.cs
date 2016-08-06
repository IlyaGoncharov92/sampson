namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Declarations", new[] { "CategoryId" });
            AlterColumn("dbo.Declarations", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Declarations", "CategoryId");
            AddForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Declarations", new[] { "CategoryId" });
            AlterColumn("dbo.Declarations", "CategoryId", c => c.Int());
            CreateIndex("dbo.Declarations", "CategoryId");
            AddForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories", "Id");
        }
    }
}
