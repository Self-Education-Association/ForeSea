namespace LST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20160313添加Visible属性 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestMatches", "Visible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestMatches", "Visible");
        }
    }
}
