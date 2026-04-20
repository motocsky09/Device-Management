using Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [HttpGet]
        [Route("GetDevices")]
        public ActionResult GetDevices()
        {
            var result = _deviceRepository.GetDevices();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDeviceById")]
        public ActionResult GetDeviceById(string id)
        {
            var result = _deviceRepository.GetDeviceById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateDevice")]
        public ActionResult CreateDevice(Device device)
        {
            _deviceRepository.CreateDevice(device);
            return Ok(device);
        }

        [HttpPut]
        [Route("UpdateDevice")]
        public ActionResult UpdateDevice(Device device)
        {
            _deviceRepository.UpdateDevice(device);
            return Ok(device);
        }

        [HttpDelete]
        [Route("DeleteDevice")]
        public ActionResult DeleteDevice(string id)
        {
            _deviceRepository.DeleteDevice(id);
            return Ok();
        }
    }
}