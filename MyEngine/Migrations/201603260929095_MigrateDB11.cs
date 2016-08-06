namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LinkName = c.String(),
                        SectionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Declarations", "CategoryId", c => c.Int());
            CreateIndex("dbo.Declarations", "CategoryId");
            AddForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Declarations", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Declarations", new[] { "CategoryId" });
            DropColumn("dbo.Declarations", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
