using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaBuilder.models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Score { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public int StatusId { get; set; }
        public ReviewStatus Status { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
