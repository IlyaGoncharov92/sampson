namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB35 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ManageUserPhotoes", "UserId", "dbo.Users");
            DropIndex("dbo.ManageUserPhotoes", new[] { "UserId" });
            DropTable("dbo.ManageUserPhotoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ManageUserPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ManageUserPhotoes", "UserId");
            AddForeignKey("dbo.ManageUserPhotoes", "UserId", "dbo.Users", "Id");
        }
    }
}
