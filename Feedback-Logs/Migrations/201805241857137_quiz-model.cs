namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quizmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Register = c.String(nullable: false),
                        Assignment = c.String(nullable: false),
                        Score = c.Int(nullable: false),
                        Condition = c.Int(nullable: false),
                        ItemOne = c.String(nullable: false),
                        ItemTwo = c.String(nullable: false),
                        ItemThree = c.String(nullable: false),
                        ItemFour = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Quizs");
        }
    }
}
