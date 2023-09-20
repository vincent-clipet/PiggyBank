using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchemaBuilder.models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? ImageUrl { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public int Weight { get; set; }

        public int Capacity { get; set; }

        [MaxLength(50)]
        [Required]
        public string Color { get; set; }

        public int Price { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer? Manufacturer { get; set; }
    }
}
