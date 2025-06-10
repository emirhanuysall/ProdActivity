using ProdActivity.Entities;

namespace ProdActivity.DataAccess.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(Guid id);
        void Add(User user);
        void Update(User user);
        void Delete(Guid id);
    }
}
