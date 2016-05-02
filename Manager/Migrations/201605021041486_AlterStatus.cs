namespace Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Status", "Name", c => c.String());
            DropColumn("dbo.Status", "AccountName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Status", "AccountName", c => c.String());
            DropColumn("dbo.Status", "Name");
        }
    }
}
