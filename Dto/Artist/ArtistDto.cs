using MusicLab_SQL.Dto.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Dto.Artist
{
    public class ArtistDto
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Des { get; set; }
        public string Pic { get; set; }
        public ICollection<SongDto> Songs { get; set; }
        public ICollection<AlbumDto> Albums { get; set; }
    }
}
