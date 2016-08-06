namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB105 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Declarations", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Declarations", "Title", c => c.String());
        }
    }
}
