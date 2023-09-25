using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PiggyBankMVC.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Required]
        [Range(0, 100)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Price { get; set; }

        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
