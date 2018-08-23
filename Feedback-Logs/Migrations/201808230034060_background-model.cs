namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backgroundmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Backgrounds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Register = c.String(nullable: false),
                        ICC = c.Boolean(nullable: false),
                        P1 = c.Boolean(nullable: false),
                        P2 = c.Boolean(nullable: false),
                        EDA = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Backgrounds");
        }
    }
}
