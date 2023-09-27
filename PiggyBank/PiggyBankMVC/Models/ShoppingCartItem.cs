using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiggyBankMVC.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int ItemId { get; set; }

        public int Quantity { get; set; }

        [Required]
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public virtual ShoppingCart? ShoppingCart { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
