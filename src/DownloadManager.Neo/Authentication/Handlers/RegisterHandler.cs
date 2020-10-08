using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DownloadManager.Data.Entities;
using DownloadManager.Entities;
using DownloadManager.Neo.Authentication.Commands;
using DownloadManager.Neo.Authentication.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DownloadManager.Neo.Authentication.Handlers
{

    public class RegisterHandler : IRequestHandler<RegisterCommand, TokenResponse>
    {
        private readonly Context _context;
        private readonly IConfiguration _config;

        public RegisterHandler(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<TokenResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (EmailTaken(request.Email))
            {
                return null;
            }

            var refreshToken = Helpers.GenerateRandomToken(128);
            var password = Helpers.HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                Email = request.Email,
                Password = password,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTime.Now.AddMinutes(10)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new TokenResponse
            {
                AccessToken = Helpers.GenerateJwt(user, _config["secret"]),
                RefreshToken = refreshToken
            };
        }

        private bool EmailTaken(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }
    }
}

