using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Dto
{
    public class SongGenreDto
    {
        public SongDto Song { get; set; }
        public GenreDto Genre { get; set; }
    }
}
