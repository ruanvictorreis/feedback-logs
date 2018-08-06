namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_agreement_to_register : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisterLogs", "AgreementRequired", c => c.Boolean(nullable: false));
            DropColumn("dbo.RegisterLogs", "Permission");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisterLogs", "Permission", c => c.Boolean(nullable: false));
            DropColumn("dbo.RegisterLogs", "AgreementRequired");
        }
    }
}
