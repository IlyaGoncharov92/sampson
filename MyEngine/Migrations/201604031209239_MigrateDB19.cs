namespace MyEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Declarations", "ExtraDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Declarations", "ExtraDescription");
        }
    }
}
