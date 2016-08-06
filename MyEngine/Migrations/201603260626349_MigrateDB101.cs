namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB101 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Declarations", "StratusDeclarationId", "dbo.StratusDeclarations");
            DropForeignKey("dbo.Towns", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Declarations", "TownId", "dbo.Towns");
            DropIndex("dbo.Categories", new[] { "SectionId" });
            DropIndex("dbo.Declarations", new[] { "CategoryId" });
            DropIndex("dbo.Declarations", new[] { "StratusDeclarationId" });
            DropIndex("dbo.Towns", new[] { "RegionId" });
            DropIndex("dbo.Declarations", new[] { "TownId" });
            AlterColumn("dbo.Declarations", "Title", c => c.String());
            AlterColumn("dbo.Declarations", "Description", c => c.String());
            AlterColumn("dbo.Declarations", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Declarations", "Name", c => c.String());
            DropColumn("dbo.Declarations", "CategoryId");
            DropColumn("dbo.Declarations", "TownId");
            DropColumn("dbo.Declarations", "StratusDeclarationId");
            DropTable("dbo.Categories");
            DropTable("dbo.Sections");
            DropTable("dbo.StratusDeclarations");
            DropTable("dbo.Towns");
            DropTable("dbo.Regions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StratusDeclarations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SectionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Declarations", "StratusDeclarationId", c => c.Int());
            AddColumn("dbo.Declarations", "TownId", c => c.Int(nullable: false));
            AddColumn("dbo.Declarations", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Declarations", "Name", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Declarations", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Declarations", "Description", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("dbo.Declarations", "Title", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Declarations", "TownId");
            CreateIndex("dbo.Towns", "RegionId");
            CreateIndex("dbo.Declarations", "StratusDeclarationId");
            CreateIndex("dbo.Declarations", "CategoryId");
            CreateIndex("dbo.Categories", "SectionId");
            AddForeignKey("dbo.Declarations", "TownId", "dbo.Towns", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Towns", "RegionId", "dbo.Regions", "Id");
            AddForeignKey("dbo.Declarations", "StratusDeclarationId", "dbo.StratusDeclarations", "Id");
            AddForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Categories", "SectionId", "dbo.Sections", "Id");
        }
    }
}
