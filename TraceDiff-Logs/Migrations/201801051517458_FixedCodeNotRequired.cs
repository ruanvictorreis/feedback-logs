namespace TraceDiff_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedCodeNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SubmissionLogs", "FixedCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubmissionLogs", "FixedCode", c => c.String(nullable: false));
        }
    }
}
