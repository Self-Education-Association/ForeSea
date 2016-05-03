namespace Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelsss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AvailableTimes", "TimeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AvailableTimes", "TimeName");
        }
    }
}
