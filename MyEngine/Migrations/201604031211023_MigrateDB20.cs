namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Declarations", "Color", c => c.String());
            AlterColumn("dbo.Declarations", "Сonsist", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Declarations", "Сonsist", c => c.Int());
            AlterColumn("dbo.Declarations", "Color", c => c.Int());
        }
    }
}
