namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisterLogs", "Assignment", c => c.String());
            AlterColumn("dbo.RegisterLogs", "DateTime", c => c.String());
            AlterColumn("dbo.SubmissionLogs", "DateTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubmissionLogs", "DateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RegisterLogs", "DateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RegisterLogs", "Assignment", c => c.String(nullable: false));
        }
    }
}
