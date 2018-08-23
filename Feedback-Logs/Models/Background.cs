using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feedback_Logs.Models
{
    public class Background
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String Register { get; set; }

        [Required]
        public bool ICC { get; set; }

        [Required]
        public bool P1 { get; set; }

        [Required]
        public bool P2 { get; set; }

        [Required]
        public bool EDA { get; set; }
    }
}