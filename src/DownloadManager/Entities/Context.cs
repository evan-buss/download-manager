using DownloadManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace DownloadManager.Entities
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
            ChangeTracker.Tracked += (object sender, EntityTrackedEventArgs e) =>
            {
                if (!e.FromQuery && e.Entry.State == EntityState.Added && e.Entry.Entity is BaseEntity entity)
                {
                    entity.Created = DateTime.Now;
                }
            };

            ChangeTracker.StateChanged += (object sender, EntityStateChangedEventArgs e) =>
            {
                if (e.NewState == EntityState.Modified && e.Entry.Entity is BaseEntity entity)
                {
                    entity.Updated = DateTime.Now;
                }
            };
        }
    }
}