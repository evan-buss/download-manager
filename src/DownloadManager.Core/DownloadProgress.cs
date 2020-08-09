using System;

namespace DownloadManager.Core
{
    public class DownloadProgress
    {
        public string FileName { get; set; }
        public DateTime StartDate { get; set; }
        public string Type { get; set; }

        // Progress is a value from 0 to 100
        public int Progress { get; set; }
    }
}