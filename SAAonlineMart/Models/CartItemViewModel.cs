using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAAonlineMart.Models
{
    public class CartItemViewModel
    {
        public int CProductId { get; set; }
        [StringLength(225, MinimumLength = 5)]
        [Required]
        public string CProductName { get; set; }
        [Range(1, 999999999)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(20, 2)")]
        [Required]
        public decimal CProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
