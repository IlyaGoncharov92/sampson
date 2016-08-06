namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB48 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LikedDeclarations", "DeclarationId_Id", c => c.Int());
            CreateIndex("dbo.LikedDeclarations", "DeclarationId_Id");
            AddForeignKey("dbo.LikedDeclarations", "DeclarationId_Id", "dbo.Declarations", "Id");
            DropColumn("dbo.LikedDeclarations", "DeclarationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LikedDeclarations", "DeclarationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.LikedDeclarations", "DeclarationId_Id", "dbo.Declarations");
            DropIndex("dbo.LikedDeclarations", new[] { "DeclarationId_Id" });
            DropColumn("dbo.LikedDeclarations", "DeclarationId_Id");
        }
    }
}
