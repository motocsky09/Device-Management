using Entities;

namespace Server.Repositories
{
    public interface IDeviceRepository
    {
        List<Device> GetDevices();
        Device GetDeviceById(string id);
        void CreateDevice(Device device);
        void UpdateDevice(Device device);
        void DeleteDevice(string id);
    }
}