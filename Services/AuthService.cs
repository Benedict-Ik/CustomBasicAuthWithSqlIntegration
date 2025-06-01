using CustomBasicAuth.Data;
using CustomBasicAuth.Helpers;
using CustomBasicAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomBasicAuth.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string username, string password);
        Task<string> LoginAsync(string username, string password);
    }


    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly PasswordHashHelper _helper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(AppDbContext db, PasswordHashHelper helper, ILogger<AuthService> logger)
        {
            _db = db;
            _helper = helper;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            if (await _db.Users.AnyAsync(u => u.Username == username))
            {
                _logger.LogWarning("Registration failed: username {Username} already exists.", username);
                return false;
            }

            var client = new User { Username = username };
            client.PasswordHash = _helper.HashPassword(client, password);

            _db.Users.Add(client);
            await _db.SaveChangesAsync();

            _logger.LogInformation("User {Username} registered successfully.", username);
            return true;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var client = await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (client == null || !_helper.VerifyPassword(client, password))
            {
                _logger.LogWarning("Failed login attempt for {Username}.", username);
                return null;
            }

            _logger.LogInformation("User {Username} logged in successfully.", username);
            return EncodeCredentials(username, password);
        }

        private string EncodeCredentials(string username, string password)
        {
            var raw = $"{username}:{password}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(raw);
            return Convert.ToBase64String(bytes);
        }
    }
}
