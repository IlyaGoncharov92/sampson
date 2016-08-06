namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB28 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RelatedDeclarations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdParent = c.Int(nullable: false),
                        IdChild = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Categories", "RelatedDeclaration_Id", c => c.Int());
            AddColumn("dbo.Declarations", "DeclarationType", c => c.String());
            CreateIndex("dbo.Categories", "RelatedDeclaration_Id");
            AddForeignKey("dbo.Categories", "RelatedDeclaration_Id", "dbo.RelatedDeclarations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "RelatedDeclaration_Id", "dbo.RelatedDeclarations");
            DropIndex("dbo.Categories", new[] { "RelatedDeclaration_Id" });
            DropColumn("dbo.Declarations", "DeclarationType");
            DropColumn("dbo.Categories", "RelatedDeclaration_Id");
            DropTable("dbo.RelatedDeclarations");
        }
    }
}
