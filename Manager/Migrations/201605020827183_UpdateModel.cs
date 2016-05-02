namespace Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CheckInTimes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CheckInTimes",
                c => new
                    {
                        CheckInTimeId = c.Guid(nullable: false),
                        CheckIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CheckInTimeId);
            
        }
    }
}
