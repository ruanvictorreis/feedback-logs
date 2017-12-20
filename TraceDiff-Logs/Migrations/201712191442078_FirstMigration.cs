namespace TraceDiff_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RegisterLogs");
            CreateTable(
                "dbo.SubmissionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Condition = c.Int(nullable: false),
                        Register = c.String(nullable: false),
                        Assignment = c.String(nullable: false),
                        FixedCode = c.String(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                        HasFix = c.Boolean(nullable: false),
                        SubmittedCode = c.String(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        LogsInteractionStr = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RegisterLogs", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.RegisterLogs", "Register", c => c.String(nullable: false));
            AddPrimaryKey("dbo.RegisterLogs", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RegisterLogs");
            AlterColumn("dbo.RegisterLogs", "Register", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.RegisterLogs", "Id");
            DropTable("dbo.SubmissionLogs");
            AddPrimaryKey("dbo.RegisterLogs", "Register");
        }
    }
}
