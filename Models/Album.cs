using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public int Release { get; set; }
        public string Pic { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
