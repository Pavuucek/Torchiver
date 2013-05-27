using System.ComponentModel.DataAnnotations;

namespace Torchiver.Archiver.DBModel
{
    public class TorrentTracker
    {
        [Key]
        public int TrackerId { get; set; }

        public string Url { get; set; }
        public virtual TorrentInfo  Torrent { get; set; }
    }
}