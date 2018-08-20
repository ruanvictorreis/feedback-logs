namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class learningSurveymodel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Surveys", newName: "LearningSurveys");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.LearningSurveys", newName: "Surveys");
        }
    }
}
