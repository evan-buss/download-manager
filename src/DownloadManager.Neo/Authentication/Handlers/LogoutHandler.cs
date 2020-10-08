using System.Threading;
using System.Threading.Tasks;
using DownloadManager.Entities;
using MediatR;

namespace DownloadManager.Neo.Authentication.Commands
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly Context _context;

        public LogoutHandler(Context context)
        {
            _context = context;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiration = null;

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}