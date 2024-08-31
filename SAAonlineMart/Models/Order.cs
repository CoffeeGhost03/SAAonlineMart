using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAAonlineMart.Models
{
    public class Order
    {
        internal decimal TotalAmount;

        [Key]
        public int OrderId { get; set; }
        [Range(1, 999999999)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(20, 2)")]
        [Required]
        public decimal OrderTotal { get; set; }
        [Display(Name = "Date of order")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
