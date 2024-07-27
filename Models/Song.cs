using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }
        public string Name { get; set; }
        public int Release { get; set; }
        public string Pic { get; set; }
        public int ArtistId { get; set; } //allow null
        public Artist Artist { get; set; }
        public int? AlbumId { get; set; } //allow null
        public Album Album { get; set; }

        //Relation
        public ICollection<SongGenre> SongGenres { get; set; }
        public ICollection<SongPlaylist> SongPlaylists { get; set; }
        //public Song()
        //{
        //    SongGenres = new List<SongGenre>();
        //}
    }
}
