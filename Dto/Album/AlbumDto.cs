using MusicLab_SQL.Dto.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Dto.Album
{
    public class AlbumDto
    {
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public int Release { get; set; }
        public string Pic { get; set; }
        public ArtistSimpleDto Artist { get; set; }
        public ICollection<SongSimpleDto> Songs { get; set; }
    }
}
