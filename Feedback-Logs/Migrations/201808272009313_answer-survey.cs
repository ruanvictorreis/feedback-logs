namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answersurvey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FixingSurveys", "Answer", c => c.String(nullable: false));
            AddColumn("dbo.LearningSurveys", "Answer", c => c.String(nullable: false));
            DropColumn("dbo.FixingSurveys", "Likert");
            DropColumn("dbo.LearningSurveys", "Likert");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LearningSurveys", "Likert", c => c.Int(nullable: false));
            AddColumn("dbo.FixingSurveys", "Likert", c => c.Int(nullable: false));
            DropColumn("dbo.LearningSurveys", "Answer");
            DropColumn("dbo.FixingSurveys", "Answer");
        }
    }
}
