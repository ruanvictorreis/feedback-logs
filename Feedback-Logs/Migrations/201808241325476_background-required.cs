namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backgroundrequired : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisterLogs", "BackgroundRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisterLogs", "BackgroundRequired");
        }
    }
}
