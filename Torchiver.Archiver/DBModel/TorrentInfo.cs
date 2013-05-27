using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Torchiver.Archiver.DBModel
{
    public class TorrentInfo
    {
        public TorrentInfo()
        {
            Files = new HashSet<TorrentFile>();
            Trackers = new HashSet<TorrentTracker>();
        }

        [Key]
        public int InfoId { get; set; }

        public string Name { get; set; }
        public string FileName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BlockSize { get; set; }
        public int BlockCount { get; set; }
        public int FileCount { get; set; }
        public long TotalSize { get; set; }
        public bool IsPrivate { get; set; }
        public string Sha { get; set; }
        public string Ed2K { get; set; }
        public string MagnetUrl { get; set; }
        public string Comment { get; set; }
        public string Publisher { get; set; }
        public string PublisherUrl { get; set; }
        public string Encoding { get; set; }
        public virtual ICollection<TorrentFile> Files { get; set; }
        public virtual ICollection<TorrentTracker> Trackers { get; set; }
        public virtual TorrentBlob TorrentBlob { get; set; }
    }
}