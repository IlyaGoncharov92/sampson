namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Towns", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Towns", "Name", c => c.Int(nullable: false));
        }
    }
}
