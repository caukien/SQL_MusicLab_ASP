using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Dto.Song
{
    public class SongCreateDto
    {
        public string Name { get; set; }
        public int Release { get; set; }
        public string Pic { get; set; }
        public int ArtistId { get; set; } // allow null
        public int? AlbumId { get; set; } // allow null
        /*public List<int> GenreIds { get; set; }*/ // list of genre ids
        public ICollection<int> SongGenres { get; set; }
    }
}
