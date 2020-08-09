using DownloadManager.Core;
using DownloadManager.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DownloadManager.Services
{
    public class PollService : BackgroundService
    {
        private readonly IHubContext<DownloadHub> _hubContext;

        public PollService(IHubContext<DownloadHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    await _hubContext.Clients.All.SendAsync("progress", 28).ConfigureAwait(false);
            //    await Task.Delay(10000).ConfigureAwait(false);
            //}

            var download1 = DummyDownload("file 1 test",TimeSpan.FromSeconds(1), stoppingToken);
            await Task.Delay(2000);
            var download2 = DummyDownload("file 2 test", TimeSpan.FromSeconds(0.85), stoppingToken);

            await Task.WhenAll(download1, download2);
            Console.WriteLine("DONE DOWNLOADING???");
        }

        private async Task DummyDownload(string fileName, TimeSpan interval, CancellationToken token)
        {
            var download = new DownloadProgress
            {
                FileName = fileName,
                StartDate = DateTime.UtcNow,
                Type = "MEGA",
                Progress = 0
            };

            while (!token.IsCancellationRequested && download.Progress < 100)
            {
                download.Progress++;
                await _hubContext.Clients.All.SendAsync("progress", download).ConfigureAwait(false);
                await Task.Delay(interval).ConfigureAwait(false);
            }
        }
    }
}