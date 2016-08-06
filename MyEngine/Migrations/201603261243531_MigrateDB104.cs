namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB104 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Declarations", "Coast", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Declarations", "Coast", c => c.Int(nullable: false));
        }
    }
}
