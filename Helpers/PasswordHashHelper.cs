using CustomBasicAuth.Models;
using Microsoft.AspNetCore.Identity;

namespace CustomBasicAuth.Helpers
{
    public class PasswordHashHelper
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string HashPassword(User client, string password)
        {
            return _hasher.HashPassword(client, password);
        }

        public bool VerifyPassword(User client, string password)
        {
            var result = _hasher.VerifyHashedPassword(client, client.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }

}
