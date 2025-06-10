using Microsoft.EntityFrameworkCore;
using ProdActivity.Entities;
using ProdActivity.DataAccess;

namespace ProdActivity.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users
                           .Include(u => u.UserRole)
                           .ToList();
        }

        public User GetById(Guid id) => _context.Users.Find(id);


        public void Add(User user)
        {
            _context.Entry(user).Reference(u => u.UserRole).CurrentValue = null;
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        public User? Login(string username, string password)
        {
            return _context.Users
                        .Include(u => u.UserRole)
                .FirstOrDefault(u => u.Name == username && u.Password == password && u.IsActive);
        }

    }
}
