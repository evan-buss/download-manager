using DownloadManager.Controllers;
using System.Threading.Tasks;

namespace DownloadManager.Services
{
    public interface IAuthService
    {
        public Task<TokenResponse> RegisterUser(LoginRequest model);

        public Task<TokenResponse> LogIn(LoginRequest model);

        public Task<bool> LogOut(long userId);
    }
}