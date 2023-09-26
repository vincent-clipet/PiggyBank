using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PiggyBankMVC.Models.Enums;

namespace PiggyBankMVC.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        [Required]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; }

        [Required]
        [Column("OrderStatus")]
        public EnumOrderStatus OrderStatus { get; set; }

        public ICollection<OrderDetail> Details { get; } = new List<OrderDetail>();
    }
}
