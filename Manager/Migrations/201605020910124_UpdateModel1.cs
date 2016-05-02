namespace Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "LogTime", c => c.DateTime(nullable: false, defaultValue: DateTime.Now));
        }

        public override void Down()
        {
            DropColumn("dbo.Logs", "LogTime");
        }
    }
}
