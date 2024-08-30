using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAAonlineMart.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [StringLength(225, MinimumLength = 5)]
        [Required]
        public string ProductName { get; set; }
        [Range(1, 999999999)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(20, 2)")]
        public decimal ProductPrice { get; set; }
        public string URL { get; set; }
    }
}
