using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Feedback_Logs.Models
{
    public class Feedback_LogsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public Feedback_LogsContext() : base("name=Feedback_LogsContext")
        {
        }

        public System.Data.Entity.DbSet<Feedback_Logs.Models.RegisterLog> RegisterLogs { get; set; }

        public System.Data.Entity.DbSet<Feedback_Logs.Models.SubmissionLog> SubmissionLogs { get; set; }

        public System.Data.Entity.DbSet<Feedback_Logs.Models.Quiz> QuizLogs { get; set; }

        public System.Data.Entity.DbSet<Feedback_Logs.Models.LearningSurvey> LearningSurveyLogs { get; set; }

        public System.Data.Entity.DbSet<Feedback_Logs.Models.AgreementRegister> AgreementLogs { get; set; }

        public System.Data.Entity.DbSet<Feedback_Logs.Models.Suggestion> SuggestionLogs { get; set; }
    }
}
