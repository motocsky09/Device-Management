using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMongoCollection<User> _users;
        private readonly IConfiguration _configuration;

        public AuthController(IMongoClient mongoClient, IConfiguration configuration)
        {
            var database = mongoClient.GetDatabase("DeviceDB");
            _users = database.GetCollection<User>("Users");
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterDto dto)
        {
            var exists = _users.Find(u => u.Email == dto.Email).FirstOrDefault();
            if (exists != null)
                return BadRequest("A user with this email already exists.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = "User",
                Location = dto.Location,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _users.InsertOne(user);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            var user = _users.Find(u => u.Email == dto.Email).FirstOrDefault();
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid email or password.");

            var token = GenerateJwtToken(user);
            return Ok(new { token, userId = user.Id, userName = user.Name });
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class RegisterDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}