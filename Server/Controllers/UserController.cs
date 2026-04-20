using Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("GetUsers")]
        public ActionResult GetUsers()
        {
            var result = _userRepository.GetUsers();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserById")]
        public ActionResult GetUserById(string id)
        {
            var result = _userRepository.GetUserById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateUser")]
        public ActionResult CreateUser(User user)
        {
            _userRepository.CreateUser(user);
            return Ok(user);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public ActionResult UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public ActionResult DeleteUser(string id)
        {
            _userRepository.DeleteUser(id);
            return Ok();
        }
    }
}