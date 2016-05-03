namespace Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AvailableTimes", "TimeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AvailableTimes", "TimeId");
        }
    }
}
