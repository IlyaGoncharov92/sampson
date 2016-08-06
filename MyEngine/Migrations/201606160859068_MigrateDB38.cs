namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB38 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ManageUserImages", "UserId", "dbo.Users");
            DropIndex("dbo.ManageUserImages", new[] { "UserId" });
            DropTable("dbo.ManageUserImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ManageUserImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ManageUserImages", "UserId");
            AddForeignKey("dbo.ManageUserImages", "UserId", "dbo.Users", "Id");
        }
    }
}
