using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Des { get; set; }
        public string Pic { get; set; }
        public ICollection<Song> Songs { get; set; }
        public ICollection<Album> Albums { get; set; }

    }
}
