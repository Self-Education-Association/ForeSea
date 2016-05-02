namespace Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class DefineDefaultValue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AvailableTimes", "Manager_ManagerId", "dbo.Managers");
            DropPrimaryKey("dbo.AvailableTimes");
            DropPrimaryKey("dbo.Logs");
            DropPrimaryKey("dbo.Managers");
            AlterColumn("dbo.AvailableTimes", "AvailableTimeId", c => c.Guid(nullable: false, defaultValueSql: "newid()"));
            AlterColumn("dbo.Logs", "LogId", c => c.Guid(nullable: false, defaultValueSql: "newid()"));
            AlterColumn("dbo.Logs", "LogTime", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2", defaultValueSql: "getdate()"));
            AlterColumn("dbo.Managers", "ManagerId", c => c.Guid(nullable: false, defaultValueSql: "newid()"));
            AlterColumn("dbo.Managers", "TotalTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Managers", "LateTime", c => c.Int(nullable: false));
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
            AlterColumn("dbo.Managers", "LateTime", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Managers", "TotalTime", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Managers", "ManagerId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Logs", "LogTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Logs", "LogId", c => c.Guid(nullable: false));
            AlterColumn("dbo.AvailableTimes", "AvailableTimeId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Managers", "ManagerId");
            AddPrimaryKey("dbo.Logs", "LogId");
            AddPrimaryKey("dbo.AvailableTimes", "AvailableTimeId");
            AddForeignKey("dbo.AvailableTimes", "Manager_ManagerId", "dbo.Managers", "ManagerId", cascadeDelete: true);
        }
    }
}
