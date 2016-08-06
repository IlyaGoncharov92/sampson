namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB49 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LikedDeclarations", "DeclarationId_Id", "dbo.Declarations");
            DropIndex("dbo.LikedDeclarations", new[] { "DeclarationId_Id" });
            DropTable("dbo.LikedDeclarations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LikedDeclarations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DeclarationId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.LikedDeclarations", "DeclarationId_Id");
            AddForeignKey("dbo.LikedDeclarations", "DeclarationId_Id", "dbo.Declarations", "Id");
        }
    }
}
