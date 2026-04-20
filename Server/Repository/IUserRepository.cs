using Entities;

namespace Server.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(string id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string id);
    }
}