namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB47 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Declarations", "rating", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Declarations", "rating");
        }
    }
}
