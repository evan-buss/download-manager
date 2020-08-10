using DownloadManager.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace DownloadManager.Hubs
{
    [Authorize]
    public class DownloadHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("CONNECTED" + Context.ConnectionId);
            var dummy = new DummyDownload();
            dummy.Download($"fake file {Guid.NewGuid()}", TimeSpan.FromSeconds(1), Clients.All);
            return base.OnConnectedAsync();
        }
    }
}