using System;

namespace DownloadManager.Data.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}