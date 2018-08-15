using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Feedback_Logs.Models
{
    public class Survey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String Register { get; set; }

        [Required]
        public String Assignment { get; set; }

        [Required]
        public int Likert { get; set; }

        [Required]
        public int Condition { get; set; }
    }
}