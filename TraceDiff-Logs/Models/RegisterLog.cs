using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TraceDiff_Logs.Models
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

        public DateTime DateTime { get; set; }

        public Boolean Permission { get; set; }
    }
}