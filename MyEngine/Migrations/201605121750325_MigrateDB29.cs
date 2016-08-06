namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB29 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "RelatedDeclaration_Id", "dbo.RelatedDeclarations");
            DropIndex("dbo.Categories", new[] { "RelatedDeclaration_Id" });
            AddColumn("dbo.Declarations", "RelatedDeclaration_Id", c => c.Int());
            CreateIndex("dbo.Declarations", "RelatedDeclaration_Id");
            AddForeignKey("dbo.Declarations", "RelatedDeclaration_Id", "dbo.RelatedDeclarations", "Id");
            DropColumn("dbo.Categories", "RelatedDeclaration_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "RelatedDeclaration_Id", c => c.Int());
            DropForeignKey("dbo.Declarations", "RelatedDeclaration_Id", "dbo.RelatedDeclarations");
            DropIndex("dbo.Declarations", new[] { "RelatedDeclaration_Id" });
            DropColumn("dbo.Declarations", "RelatedDeclaration_Id");
            CreateIndex("dbo.Categories", "RelatedDeclaration_Id");
            AddForeignKey("dbo.Categories", "RelatedDeclaration_Id", "dbo.RelatedDeclarations", "Id");
        }
    }
}
