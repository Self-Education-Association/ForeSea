namespace Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateModel123 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AvailableTimes", "Manager_ManagerId", "dbo.Managers");
            DropPrimaryKey("dbo.AvailableTimes");
            DropPrimaryKey("dbo.Logs");
            DropPrimaryKey("dbo.Managers");
            AlterColumn("dbo.AvailableTimes", "AvailableTimeId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Logs", "LogId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Logs", "LogTime", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2", defaultValueSql: "getdate()"));
            AlterColumn("dbo.Managers", "ManagerId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.AvailableTimes", "AvailableTimeId");
            AddPrimaryKey("dbo.Logs", "LogId");
            AddPrimaryKey("dbo.Managers", "ManagerId");
            AddForeignKey("dbo.AvailableTimes", "Manager_ManagerId", "dbo.Managers", "ManagerId", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.AvailableTimes", "Manager_ManagerId", "dbo.Managers");
            DropPrimaryKey("dbo.Managers");
            DropPrimaryKey("dbo.Logs");
            DropPrimaryKey("dbo.AvailableTimes");
            AlterColumn("dbo.Managers", "ManagerId", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Logs", "LogTime", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.Logs", "LogId", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.AvailableTimes", "AvailableTimeId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Managers", "ManagerId");
            AddPrimaryKey("dbo.Logs", "LogId");
            AddPrimaryKey("dbo.AvailableTimes", "AvailableTimeId");
            AddForeignKey("dbo.AvailableTimes", "Manager_ManagerId", "dbo.Managers", "ManagerId", cascadeDelete: true);
        }
    }
}
