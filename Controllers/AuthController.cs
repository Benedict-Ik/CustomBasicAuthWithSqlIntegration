using CustomBasicAuth.Models;
using CustomBasicAuth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomBasicAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthDto model)
        {
            var success = await _authService.RegisterAsync(model.Username, model.Password);
            
            if (!char.IsUpper(model.Username[0]))
                return BadRequest("Username must start with a capital letter.");

            // Validate password strength
            if (!ValidatePassword(model.Password))
                return BadRequest("Password does not meet security requirements: at least 1 uppercase, 1 lowercase, 1 digit, 1 special character, minimum 8 characters.");

            if (!success)
                return BadRequest("Username already exists.");

            return Ok("User registered.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthDto model)
        {
            var token = await _authService.LoginAsync(model.Username, model.Password);
            if (token == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new { 
                Message = $"{model.Username} has successfully logged in.",
                Token = token });
        }


        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasLower && hasDigit && hasSpecial;
        }
    }
}
