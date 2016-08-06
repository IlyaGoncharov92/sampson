namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Declarations", "Title", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Declarations", "Title", c => c.String(nullable: false));
        }
    }
}
