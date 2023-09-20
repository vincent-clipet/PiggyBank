using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaBuilder.models
{
    public class Manufacturer
    {
        [Key]
        public int ManufacturerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; }
    }
}
