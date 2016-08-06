namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sections", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Sections", new[] { "CategoryId" });
            DropIndex("dbo.Declarations", new[] { "CategoryId" });
            DropColumn("dbo.Declarations", "CategoryId");
            DropTable("dbo.Categories");
            DropTable("dbo.Sections");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Declarations", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Declarations", "CategoryId");
            CreateIndex("dbo.Sections", "CategoryId");
            AddForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sections", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
