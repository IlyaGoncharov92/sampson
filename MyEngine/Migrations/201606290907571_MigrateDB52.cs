namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB52 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Declarations", "Rating", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Declarations", "Rating");
        }
    }
}
