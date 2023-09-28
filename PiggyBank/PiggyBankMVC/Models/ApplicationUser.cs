using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PiggyBankMVC.Models
{
    [AllowAnonymous]
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

        [Required]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; }



        public static string? GetUserId(ClaimsPrincipal u)
        {
            return u?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
