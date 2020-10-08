using MediatR;

namespace DownloadManager.Neo.Authentication.Commands
{
    public class LogoutCommand : IRequest<bool>
    {
        public long UserId { get; set; }
    }
}