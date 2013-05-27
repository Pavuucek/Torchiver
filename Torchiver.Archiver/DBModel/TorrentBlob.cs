using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Torchiver.Archiver.DBModel
{
    public class TorrentBlob
    {
        [Key,ForeignKey("TorrentInfo")]
        public int BlobId { get; set; }

        public string Name { get; set; }
        public byte[] Data { get; set; }
        
        public virtual TorrentInfo TorrentInfo { get; set; }
    }
}