namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB44 : DbMigration
    {
        public override void Up()
        {
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
                        DeclarationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
