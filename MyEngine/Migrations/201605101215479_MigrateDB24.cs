namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB24 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeclarationColors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        ExtraDescription = c.String(),
                        Name = c.String(),
                        Color = c.String(),
                        Сonsist = c.String(),
                        Coast = c.Int(),
                        Article = c.Int(),
                        Size = c.Int(),
                        CategoryId = c.Int(nullable: false),
                        PublicDate = c.DateTime(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeclarationColors", "UserId", "dbo.Users");
            DropForeignKey("dbo.DeclarationColors", "CategoryId", "dbo.Categories");
            DropIndex("dbo.DeclarationColors", new[] { "UserId" });
            DropIndex("dbo.DeclarationColors", new[] { "CategoryId" });
            DropTable("dbo.DeclarationColors");
        }
    }
}
