namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB41 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ManagePhotoUsers",
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
            DropForeignKey("dbo.ManagePhotoUsers", "UserId", "dbo.Users");
            DropIndex("dbo.ManagePhotoUsers", new[] { "UserId" });
            DropTable("dbo.ManagePhotoUsers");
        }
    }
}
