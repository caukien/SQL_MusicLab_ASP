using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicLab_SQL.Models;

namespace MusicLab_SQL.Dto
{
    public class GenreDto
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public ICollection<SongDto> Song { get; set; }
    }
}
