using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feedback_Logs.Models
{
    public class RegisterLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String Register { get; set; }

        [Required]
        public String Assignment { get; set; }

        public int Condition { get; set; }

        public int Counter { get; set; }

        public String DateTime { get; set; }

        public Boolean AgreementRequired { get; set; }

        public Boolean BackgroundRequired { get; set; }
    }
}