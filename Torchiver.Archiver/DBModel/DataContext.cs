using System.Data.Entity;

namespace Torchiver.Archiver.DBModel
{
    public class DataContext:DbContext
    {
        public DbSet<TorrentInfo> Infos { get; set; }
        public DbSet<TorrentBlobs> Blobs { get; set; }
        public DbSet<TorrentFiles> Files { get; set; }
        public DbSet<TorrentTrackers> Trackers { get; set; }
    }
}
