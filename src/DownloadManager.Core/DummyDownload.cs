using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DownloadManager.Core
{
    public class DummyDownload
    {
        public async Task Download(string fileName, TimeSpan interval, IClientProxy proxy, CancellationToken? token = null)
        {
            var download = new DownloadProgress
            {
                FileName = fileName,
                StartDate = DateTime.UtcNow,
                Type = "MEGA",
                Progress = 0
            };

            while (!token?.IsCancellationRequested ?? download.Progress < 100)
            {
                download.Progress++;
                await proxy.SendAsync("progress", download).ConfigureAwait(false);
                await Task.Delay(interval).ConfigureAwait(false);
            }

            Console.WriteLine("DONE?");
        }
    }
}