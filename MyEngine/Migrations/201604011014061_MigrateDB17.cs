namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SectionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Declarations", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Declarations", "CategoryId");
            AddForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "SectionId", "dbo.Sections");
            DropIndex("dbo.Declarations", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "SectionId" });
            DropColumn("dbo.Declarations", "CategoryId");
            DropTable("dbo.Sections");
            DropTable("dbo.Categories");
        }
    }
}
