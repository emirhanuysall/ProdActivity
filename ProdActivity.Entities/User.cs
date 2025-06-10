using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ProdActivity.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int UserRoleId { get; set; }

        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public virtual UserRole UserRole { get; set; }
    }
}
