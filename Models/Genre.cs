using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        //Relation
        public ICollection<SongGenre> SongGenres { get; set; }
        //public Genre()
        //{
        //    SongGenres = new List<SongGenre>();
        //}
    }
}
