using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Dto.Album
{
    public class AlbumCreateDto
    {
        public string Name { get; set; }
        public int Release { get; set; }
        public string Pic { get; set; }
        public int ArtistId { get; set; }
        public ICollection<int> SongIds { get; set; }
    }
}
