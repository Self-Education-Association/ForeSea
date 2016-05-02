namespace Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Status", "TimeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Status", "TimeName");
        }
    }
}
