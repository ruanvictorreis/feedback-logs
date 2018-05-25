using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Feedback_Logs.Models
{
    public class SubmissionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String DateTime { get; set; }

        [Required]
        public String Register { get; set; }

        [Required]
        public String Assignment { get; set; }

        [Required]
        public int Condition { get; set; }


        public Boolean IsCorrect { get; set; }


        public String SubmittedCode { get; set; }


        public Boolean SyntaxError { get; set; }


        public String ErrorMsg { get; set; }


        public String CodeRepaired { get; set; }


        public Boolean IsCodeRepaired { get; set; }


        public String Repairs { get; set; }


        public Boolean IsRepaired { get; set; }
    }
}