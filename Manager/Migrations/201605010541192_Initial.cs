namespace Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailableTimes",
                c => new
                {
                    AvailableTimeId = c.Guid(nullable: false),
                    Demand = c.Int(nullable: false),
                    Manager_ManagerId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.AvailableTimeId)
                .ForeignKey("dbo.Managers", t => t.Manager_ManagerId, cascadeDelete: false)
                .Index(t => t.Manager_ManagerId);

            CreateTable(
                "dbo.CheckInTimes",
                c => new
                {
                    CheckInTimeId = c.Guid(nullable: false),
                    CheckIn = c.DateTime(nullable: false),
                    CheckOut = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.CheckInTimeId);

            CreateTable(
                "dbo.Logs",
                c => new
                {
                    LogId = c.Guid(nullable: false),
                    LogContent = c.String(),
                })
                .PrimaryKey(t => t.LogId);

            CreateTable(
                "dbo.Managers",
                c => new
                {
                    ManagerId = c.Guid(nullable: false),
                    Name = c.String(),
                    AccountName = c.String(),
                    MinCount = c.Int(nullable: false),
                    MaxCount = c.Int(nullable: false),
                    TotalCount = c.Int(nullable: false),
                    TotalTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                    LateTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.ManagerId);

            CreateTable(
                "dbo.Status",
                c => new
                {
                    StatusId = c.Guid(nullable: false),
                    AccountName = c.String(),
                })
                .PrimaryKey(t => t.StatusId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AvailableTimes", "Manager_ManagerId", "dbo.Managers");
            DropIndex("dbo.AvailableTimes", new[] { "Manager_ManagerId" });
            DropTable("dbo.Status");
            DropTable("dbo.Managers");
            DropTable("dbo.Logs");
            DropTable("dbo.CheckInTimes");
            DropTable("dbo.AvailableTimes");
        }
    }
}
