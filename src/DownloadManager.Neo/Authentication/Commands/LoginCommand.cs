using DownloadManager.Neo.Authentication.Models;
using MediatR;

namespace DownloadManager.Neo.Authentication.Commands
{
    public class LoginCommand : IRequest<TokenResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}