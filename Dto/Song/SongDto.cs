using MusicLab_SQL.Dto.Album;
using MusicLab_SQL.Dto.Artist;
using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Dto
{
    public class SongDto
    {
        public int SongId { get; set; }
        public string Name { get; set; }
        public DateTime Release { get; set; }
        public ArtistDto Artist { get; set; }
        public AlbumDto Album { get; set; }

        //Relation
        public ICollection<GenreDto> Genre { get; set; }
    }
}
