using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Models
{
    public class Playlist
    {
        [Key]
        public int PlaylistId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<SongPlaylist> SongPlaylists { get; set; }
    }
}
