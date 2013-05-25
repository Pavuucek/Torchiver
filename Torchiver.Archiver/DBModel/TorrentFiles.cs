using System.ComponentModel.DataAnnotations;

namespace Torchiver.Archiver.DBModel
{
    public class TorrentFiles
    {
        [Key]
        public int FileId { get; set; }
        public TorrentInfo Torrent { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public string Ed2K { get; set; }
        public int StartPiece { get; set; }
        public int EndPiece { get; set; }
        public string Md5 { get; set; }
        public string Sha { get; set; }
    }
}