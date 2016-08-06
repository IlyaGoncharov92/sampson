namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StratusDeclarations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Declarations", "StratusDeclarationId", c => c.Int());
            CreateIndex("dbo.Declarations", "StratusDeclarationId");
            AddForeignKey("dbo.Declarations", "StratusDeclarationId", "dbo.StratusDeclarations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Declarations", "StratusDeclarationId", "dbo.StratusDeclarations");
            DropIndex("dbo.Declarations", new[] { "StratusDeclarationId" });
            DropColumn("dbo.Declarations", "StratusDeclarationId");
            DropTable("dbo.StratusDeclarations");
        }
    }
}
