using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Feedback_Logs.Models
{
    public class AgreementRegister
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String Register { get; set; }

        [Required]
        public Boolean Acceptance { get; set; }
    }
}