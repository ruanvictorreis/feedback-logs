namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisterLogs",
                c => new
                    {
                        Register = c.String(nullable: false, maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                        Assignment = c.String(nullable: false),
                        Condition = c.Int(nullable: false),
                        Permission = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Register);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegisterLogs");
        }
    }
}
