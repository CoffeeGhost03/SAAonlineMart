using System.ComponentModel.DataAnnotations;

namespace SAAonlineMart.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        [StringLength(225, MinimumLength = 5)]
        [Required]
        public string AdminMail { get; set; }
        [StringLength(225, MinimumLength = 5)]
        [Required]
        public string AdminPassword { get; set; }
    }
}
