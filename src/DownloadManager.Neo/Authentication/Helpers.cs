using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DownloadManager.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DownloadManager.Neo.Authentication
{
    internal static class Helpers
    {

        internal static readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        internal static string GenerateJwt(User user, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.FirstName),
                    new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        internal static bool VerifyPassword(string password, string hashedPassword)
        {
            var success = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return success == PasswordVerificationResult.Success;
        }

        internal static string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        internal static string GenerateRandomToken(int bits)
        {
            byte[] output = new byte[bits / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(output);
            }
            return Convert.ToBase64String(output);
        }
    }
}