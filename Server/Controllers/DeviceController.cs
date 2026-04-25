using Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly OllamaService _ollamaService;

        public DeviceController(IDeviceRepository deviceRepository, OllamaService ollamaService)
        {
            _deviceRepository = deviceRepository;
            _ollamaService = ollamaService;
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
        
        [HttpPut]
        [Route("AssignDevice")]
        public ActionResult AssignDevice(string deviceId, string userId)
        {
            var device = _deviceRepository.GetDeviceById(deviceId);
            if (device == null) return NotFound();
            if (device.AssignedUserId != null) return BadRequest("Device is already assigned.");
    
            device.AssignedUserId = userId;
            _deviceRepository.UpdateDevice(device);
            return Ok(device);
        }

        [HttpPut]
        [Route("UnassignDevice")]
        public ActionResult UnassignDevice(string deviceId, string userId)
        {
            var device = _deviceRepository.GetDeviceById(deviceId);
            if (device == null) return NotFound();
            if (device.AssignedUserId != userId) return BadRequest("You can only unassign your own device.");
    
            device.AssignedUserId = null;
            _deviceRepository.UpdateDevice(device);
            return Ok(device);
        }
        
        [HttpPost]
        [Route("GenerateDescription")]
        public async Task<ActionResult> GenerateDescription([FromBody] Device device)
        {
            var description = await _ollamaService.GenerateDeviceDescription(
                device.Name,
                device.Manufacturer,
                device.Type,
                device.OperatingSystem,
                device.Processor,
                device.Ram
            );
            return Ok(new { description });
        }
    }
}