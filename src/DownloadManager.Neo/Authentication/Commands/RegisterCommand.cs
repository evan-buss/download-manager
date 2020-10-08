using DownloadManager.Neo.Authentication.Models;
using MediatR;

namespace DownloadManager.Neo.Authentication.Commands
{
    public class RegisterCommand : IRequest<TokenResponse>
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}