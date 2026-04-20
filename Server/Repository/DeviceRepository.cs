using Entities;
using MongoDB.Driver;

namespace Server.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IMongoCollection<Device> _collection;

        public DeviceRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("DeviceDB");
            _collection = database.GetCollection<Device>("Devices");
        }

        public List<Device> GetDevices()
        {
            return _collection.Find(_ => true).ToList();
        }

        public Device GetDeviceById(string id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public void CreateDevice(Device device)
        {
            _collection.InsertOne(device);
        }

        public void UpdateDevice(Device device)
        {
            _collection.ReplaceOne(x => x.Id == device.Id, device);
        }

        public void DeleteDevice(string id)
        {
            _collection.DeleteOne(x => x.Id == id);
        }
    }
}