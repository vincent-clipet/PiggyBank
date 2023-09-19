using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaBuilder.models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(100), MinLength(6)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256), MinLength(3)]
        public string Password { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [Required]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
    }
}
