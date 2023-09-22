using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PiggyBankMVC.Models
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
