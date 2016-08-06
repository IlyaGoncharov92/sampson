namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB51 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Declarations", "rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Declarations", "rating", c => c.Int());
        }
    }
}
