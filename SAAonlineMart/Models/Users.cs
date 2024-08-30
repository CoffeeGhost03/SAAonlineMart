using System.ComponentModel.DataAnnotations;

namespace SAAonlineMart.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [StringLength(225, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(225, MinimumLength = 3)]
        public string Surname { get; set; }
        [StringLength(225, MinimumLength = 5)]
        [Required]
        public string Email { get; set; }
        [StringLength(225, MinimumLength = 5)]
        [Required]
        public string Password { get; set; }

    }
}
