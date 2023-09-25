using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PiggyBankMVC.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? ImageUrl { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Height { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Width { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Length { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Weight { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Capacity { get; set; }

        [MaxLength(50)]
        [Required]
        public string Color { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Price { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer? Manufacturer { get; set; }



        public float GetRealPrice()
        {
            return this.Price / 100;
        }
    }
}
