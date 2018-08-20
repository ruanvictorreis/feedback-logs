namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingSurveymodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FixingSurveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Register = c.String(nullable: false),
                        Assignment = c.String(nullable: false),
                        Likert = c.Int(nullable: false),
                        Condition = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FixingSurveys");
        }
    }
}
