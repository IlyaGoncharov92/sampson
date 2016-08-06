namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB25 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeclarationColors", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories");
            DropIndex("dbo.DeclarationColors", new[] { "CategoryId" });
            DropIndex("dbo.Declarations", new[] { "CategoryId" });
            AddColumn("dbo.DeclarationColors", "DeclarationColorId", c => c.Int());
            AddColumn("dbo.DeclarationColors", "Declaration_Id", c => c.Int());
            AlterColumn("dbo.DeclarationColors", "CategoryId", c => c.Int());
            AlterColumn("dbo.Declarations", "CategoryId", c => c.Int());
            CreateIndex("dbo.DeclarationColors", "Declaration_Id");
            CreateIndex("dbo.DeclarationColors", "CategoryId");
            CreateIndex("dbo.Declarations", "CategoryId");
            AddForeignKey("dbo.DeclarationColors", "Declaration_Id", "dbo.Declarations", "Id");
            AddForeignKey("dbo.DeclarationColors", "CategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.DeclarationColors", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.DeclarationColors", "Declaration_Id", "dbo.Declarations");
            DropIndex("dbo.Declarations", new[] { "CategoryId" });
            DropIndex("dbo.DeclarationColors", new[] { "CategoryId" });
            DropIndex("dbo.DeclarationColors", new[] { "Declaration_Id" });
            AlterColumn("dbo.Declarations", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.DeclarationColors", "CategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.DeclarationColors", "Declaration_Id");
            DropColumn("dbo.DeclarationColors", "DeclarationColorId");
            CreateIndex("dbo.Declarations", "CategoryId");
            CreateIndex("dbo.DeclarationColors", "CategoryId");
            AddForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DeclarationColors", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
