using ProdActivity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProdActivity.DataAccess.Repositories
{
    public class UserRoleRepository
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<UserRole> GetAll()
        {
            return _context.UserRoles.ToList();
        }
    }
}
