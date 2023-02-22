using Core.Domain;

namespace Core.Abstractions.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        User GetUserByUserName (string userName);
        User GetUserByEmail(string email);
        bool Insert(User user);
        bool Delete(int id);
        bool Update(int userId, User user); 



    }
}
