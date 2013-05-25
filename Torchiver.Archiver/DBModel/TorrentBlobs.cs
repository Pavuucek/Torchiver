using System.ComponentModel.DataAnnotations;

namespace Torchiver.Archiver.DBModel
{
    public class TorrentBlobs
    {
        [Key]
        public int BlobId { get; set; }
        public TorrentInfo Torrent { get; set; }
        public string Name { get; set; }
        public byte[] Blob { get; set; }
    }
}