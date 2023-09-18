using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaBuilder.models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public int Score { get; set; }

        [MaxLength(2000), MinLength(5)]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public ReviewStatus Status { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
