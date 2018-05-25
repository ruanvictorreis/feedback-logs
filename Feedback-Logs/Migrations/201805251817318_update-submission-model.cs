namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesubmissionmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubmissionLogs", "SyntaxError", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubmissionLogs", "ErrorMsg", c => c.String());
            AddColumn("dbo.SubmissionLogs", "CodeRepaired", c => c.String());
            AddColumn("dbo.SubmissionLogs", "IsCodeRepaired", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubmissionLogs", "Repairs", c => c.String());
            AddColumn("dbo.SubmissionLogs", "IsRepaired", c => c.Boolean(nullable: false));
            AlterColumn("dbo.SubmissionLogs", "SubmittedCode", c => c.String());
            AlterColumn("dbo.SubmissionLogs", "DateTime", c => c.String(nullable: false));
            DropColumn("dbo.SubmissionLogs", "FixedCode");
            DropColumn("dbo.SubmissionLogs", "HasFix");
            DropColumn("dbo.SubmissionLogs", "LogsInteractionStr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubmissionLogs", "LogsInteractionStr", c => c.String());
            AddColumn("dbo.SubmissionLogs", "HasFix", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubmissionLogs", "FixedCode", c => c.String());
            AlterColumn("dbo.SubmissionLogs", "DateTime", c => c.String());
            AlterColumn("dbo.SubmissionLogs", "SubmittedCode", c => c.String(nullable: false));
            DropColumn("dbo.SubmissionLogs", "IsRepaired");
            DropColumn("dbo.SubmissionLogs", "Repairs");
            DropColumn("dbo.SubmissionLogs", "IsCodeRepaired");
            DropColumn("dbo.SubmissionLogs", "CodeRepaired");
            DropColumn("dbo.SubmissionLogs", "ErrorMsg");
            DropColumn("dbo.SubmissionLogs", "SyntaxError");
        }
    }
}
