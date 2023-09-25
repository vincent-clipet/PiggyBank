using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PiggyBankMVC.Models.Enums;

namespace PiggyBankMVC.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public int Score { get; set; }

        [MaxLength(2000), MinLength(5)]
        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        [Required]
        [Column("ReviewStatus")]
        public EnumReviewStatus ReviewStatus { get; set; }
    }
}
