using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaBuilder.models
{
    public  class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
