using DownloadManager.Entities;
using DownloadManager.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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

        public AuthService(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<TokenResponse> LogIn(LoginRequest model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == model.Username).ConfigureAwait(false);
            if (user == null) return null;

            if (!VerifyPassword(model.Password, user.Password, user.Salt)) return null;

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

            var salt = GenerateRandomToken(128);
            var refreshToken = GenerateRandomToken(128);
            var password = HashPassword(model.Password, salt);

            var user = new User
            {
                Username = model.Username,
                Password = password,
                Salt = salt,
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
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password, string salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10_000,
                numBytesRequested: 256 / 8));
        }

        private bool VerifyPassword(string password, string hash, string salt)
        {
            return HashPassword(password, salt) == hash;
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
            return _context.Users.Any(x => x.Username == username);
        }
    }
}