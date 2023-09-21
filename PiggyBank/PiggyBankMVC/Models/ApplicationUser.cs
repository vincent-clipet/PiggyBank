using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PiggyBankMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        public bool IsActive { get; set; }

        //[Required]
        //public int AddressId { get; set; }
        //[ForeignKey("AddressId")]
        //public virtual Address? Address { get; set; }
    }
}
