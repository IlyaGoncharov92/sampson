namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB37 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ManageUserImages",
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
            DropForeignKey("dbo.ManageUserImages", "UserId", "dbo.Users");
            DropIndex("dbo.ManageUserImages", new[] { "UserId" });
            DropTable("dbo.ManageUserImages");
        }
    }
}
