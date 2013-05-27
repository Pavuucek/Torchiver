using System.Data.Entity;

namespace Torchiver.Archiver.DBModel
{
    public class DataContext : DbContext
    {
        public DbSet<TorrentInfo> Infos { get; set; }
        public DbSet<TorrentFile> Files { get; set; }
        public DbSet<TorrentTracker> Trackers { get; set; }
        public DbSet<TorrentBlob> Blobs { get; set; }
    }
}
