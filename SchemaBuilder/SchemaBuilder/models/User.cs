using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaBuilder.models
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
