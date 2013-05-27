using System.ComponentModel.DataAnnotations;

namespace Torchiver.Archiver.DBModel
{
    public class TorrentFile
    {
        [Key]
        public int FileId { get; set; }

        public string Path { get; set; }
        public long Size { get; set; }
        public string Ed2K { get; set; }
        public int StartBlock { get; set; }
        public int EndBlock { get; set; }
        public string Sha { get; set; }
        public string Md5 { get; set; }
        public virtual TorrentInfo Info { get; set; }
    }
}