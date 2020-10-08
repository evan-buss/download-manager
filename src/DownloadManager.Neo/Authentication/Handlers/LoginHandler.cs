using DownloadManager.Entities;
using DownloadManager.Neo.Authentication.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DownloadManager.Neo.Authentication.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, TokenResponse>
    {
        private readonly Context _context;
        private readonly IConfiguration _config;

        public LoginHandler(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == request.Email);

            if (user == null) return null;

            if (!Helpers.VerifyPassword(request.Password, user.Password)) return null;

            var refreshToken = Helpers.GenerateRandomToken(128);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddMinutes(10);

            _context.Users.Update(user);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new TokenResponse
            {
                AccessToken = Helpers.GenerateJwt(user, _config["Secret"]),
                RefreshToken = refreshToken
            };
        }
    }
}