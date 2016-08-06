namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB43 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LikedDeclarations", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.LikedDeclarations");
            AddPrimaryKey("dbo.LikedDeclarations", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.LikedDeclarations");
            AddPrimaryKey("dbo.LikedDeclarations", "id");
            AlterColumn("dbo.LikedDeclarations", "Id", c => c.Int(nullable: false, identity: true));
        }
    }
}
