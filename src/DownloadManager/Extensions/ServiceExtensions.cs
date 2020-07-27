using DownloadManager.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DownloadManager.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
        }
    }
}