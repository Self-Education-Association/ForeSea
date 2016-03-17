namespace LST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentNameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StudentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "StudentName");
        }
    }
}
