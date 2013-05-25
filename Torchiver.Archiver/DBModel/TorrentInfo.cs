using System;
using System.ComponentModel.DataAnnotations;

namespace Torchiver.Archiver.DBModel
{
    public class TorrentInfo
    {
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
    }
}