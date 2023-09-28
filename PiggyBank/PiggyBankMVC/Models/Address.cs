using System.ComponentModel.DataAnnotations;

namespace PiggyBankMVC.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Number { get; set; }

        [Required]
        [MaxLength(150)]
        public string Street { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Zip { get; set; }

        public override string ToString()
        {
            return Number + " " + Street + ". " + Zip + " " + "City";
        }
    }
}
