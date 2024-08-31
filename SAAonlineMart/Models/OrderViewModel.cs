using SAAonlineMart.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class OrderViewModel
{
    public int OrderId { get; set; }
    [StringLength(225, MinimumLength = 5)]
    [Required]
    public List<CartItemViewModel> Items { get; set; }
    [Range(1, 999999999)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(20, 2)")]
    [Required]
    public decimal TotalAmount { get; set; }
    [Display(Name = "Date of order")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime OrderDate { get; set; }
}
