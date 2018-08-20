namespace Feedback_Logs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suggestionmodel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Preferences", newName: "Suggestions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Suggestions", newName: "Preferences");
        }
    }
}
