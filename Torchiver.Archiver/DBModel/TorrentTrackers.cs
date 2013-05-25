using System.ComponentModel.DataAnnotations;

namespace Torchiver.Archiver.DBModel
{
    public class TorrentTrackers
    {
        [Key]
        public int TrackerId { get; set; }
        public TorrentInfo Torrent { get; set; }
        public string Url { get; set; }
    }
}