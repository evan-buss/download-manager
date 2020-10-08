using DownloadManager.Data.Entities;
using DownloadManager.Entities;
using DownloadManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly Context _context;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<string> _passwordHasher;

        public AuthService(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _passwordHasher = new PasswordHasher<string>();
        }

        public async Task<TokenResponse> LogIn(LoginRequest model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.FirstName == model.Username).ConfigureAwait(false);
            if (user == null) return null;

            if (!VerifyPassword(model.Password, user.Password)) return null;

            var refreshToken = GenerateRandomToken(128);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddMinutes(10);

            _context.Users.Update(user);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new TokenResponse
            {
                AccessToken = GenerateJwt(user),
                RefreshToken = refreshToken
            };
        }

        public async Task<bool> LogOut(long userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiration = null;

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<TokenResponse> RegisterUser(LoginRequest model)
        {
            if (UsernameTaken(model.Username))
            {
                return null;
            }

            var refreshToken = GenerateRandomToken(128);
            var password = _passwordHasher.HashPassword(null, model.Password);

            var user = new User
            {
                FirstName = model.Username,
                Password = password,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTime.Now.AddMinutes(10)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new TokenResponse
            {
                AccessToken = GenerateJwt(user),
                RefreshToken = refreshToken
            };
        }

        private string GenerateJwt(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Secret"]);
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

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var success = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return success == PasswordVerificationResult.Success;
        }

        private string GenerateRandomToken(int bits)
        {
            byte[] output = new byte[bits / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(output);
            }
            return Convert.ToBase64String(output);
        }

        private bool UsernameTaken(string username)
        {
            return _context.Users.Any(x => x.FirstName == username);
        }
    }
}