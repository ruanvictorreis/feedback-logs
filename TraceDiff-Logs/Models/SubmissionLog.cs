using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TraceDiff_Logs.Models
{
    public class SubmissionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Condition { get; set; }

        [Required]
        public String Register { get; set; }

        [Required]
        public String Assignment { get; set; }

        public String FixedCode { get; set; }

        [Required]
        public Boolean IsCorrect { get; set; }

        [Required]
        public Boolean HasFix { get; set; }

        [Required]
        public String SubmittedCode { get; set; }

        public String DateTime { get; set; }

        public String LogsInteractionStr { get; set; }

        public List<String> LogsInteractionList { get; set; }
    }
}