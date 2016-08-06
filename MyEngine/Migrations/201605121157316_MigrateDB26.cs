namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB26 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeclarationColors", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.DeclarationColors", "Declaration_Id", "dbo.Declarations");
            DropForeignKey("dbo.DeclarationColors", "UserId", "dbo.Users");
            DropIndex("dbo.DeclarationColors", new[] { "CategoryId" });
            DropIndex("dbo.DeclarationColors", new[] { "Declaration_Id" });
            DropIndex("dbo.DeclarationColors", new[] { "UserId" });
            DropTable("dbo.DeclarationColors");
        }
        
        public override void Down()
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
                        CategoryId = c.Int(),
                        PublicDate = c.DateTime(nullable: false),
                        UserId = c.Int(),
                        DeclarationColorId = c.Int(),
                        Declaration_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.DeclarationColors", "UserId");
            CreateIndex("dbo.DeclarationColors", "Declaration_Id");
            CreateIndex("dbo.DeclarationColors", "CategoryId");
            AddForeignKey("dbo.DeclarationColors", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.DeclarationColors", "Declaration_Id", "dbo.Declarations", "Id");
            AddForeignKey("dbo.DeclarationColors", "CategoryId", "dbo.Categories", "Id");
        }
    }
}
