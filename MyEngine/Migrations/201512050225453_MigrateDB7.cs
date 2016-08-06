namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Declarations", "TownId", c => c.Int());
            CreateIndex("dbo.Declarations", "TownId");
            AddForeignKey("dbo.Declarations", "TownId", "dbo.Towns", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Declarations", "TownId", "dbo.Towns");
            DropIndex("dbo.Declarations", new[] { "TownId" });
            DropColumn("dbo.Declarations", "TownId");
            DropTable("dbo.Towns");
        }
    }
}
