namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB50 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikedDeclarations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DeclarationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Declarations", t => t.DeclarationId, cascadeDelete: true)
                .Index(t => t.DeclarationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikedDeclarations", "DeclarationId", "dbo.Declarations");
            DropIndex("dbo.LikedDeclarations", new[] { "DeclarationId" });
            DropTable("dbo.LikedDeclarations");
        }
    }
}
