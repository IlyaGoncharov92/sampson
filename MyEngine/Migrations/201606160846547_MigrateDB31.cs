namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB31 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ManageUserPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ManageUserPhotoes", "UserId", "dbo.Users");
            DropIndex("dbo.ManageUserPhotoes", new[] { "UserId" });
            DropTable("dbo.ManageUserPhotoes");
        }
    }
}
