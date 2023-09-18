using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaBuilder.models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public int StatusId { get; set; }
        public virtual OrderStatus Status { get; set; }

    }
}
