namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class countertoregister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisterLogs", "Counter", c => c.Int(nullable: false));
            AlterColumn("dbo.RegisterLogs", "Assignment", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterLogs", "Assignment", c => c.String());
            DropColumn("dbo.RegisterLogs", "Counter");
        }
    }
}
