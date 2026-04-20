using Entities;
using MongoDB.Driver;

namespace Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("DeviceDB");
            _collection = database.GetCollection<User>("Users");
        }

        public List<User> GetUsers()
        {
            return _collection.Find(_ => true).ToList();
        }

        public User GetUserById(string id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            _collection.InsertOne(user);
        }

        public void UpdateUser(User user)
        {
            _collection.ReplaceOne(x => x.Id == user.Id, user);
        }

        public void DeleteUser(string id)
        {
            _collection.DeleteOne(x => x.Id == id);
        }
    }
}