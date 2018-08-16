namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class preferencemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Preferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Register = c.String(nullable: false),
                        Feedback = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Preferences");
        }
    }
}
