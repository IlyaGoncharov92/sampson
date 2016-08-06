namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB30 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Declarations", "RelatedDeclaration_Id", "dbo.RelatedDeclarations");
            DropIndex("dbo.Declarations", new[] { "RelatedDeclaration_Id" });
            DropColumn("dbo.Declarations", "RelatedDeclaration_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Declarations", "RelatedDeclaration_Id", c => c.Int());
            CreateIndex("dbo.Declarations", "RelatedDeclaration_Id");
            AddForeignKey("dbo.Declarations", "RelatedDeclaration_Id", "dbo.RelatedDeclarations", "Id");
        }
    }
}
