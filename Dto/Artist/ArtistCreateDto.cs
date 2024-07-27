using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Dto.Artist
{
    public class ArtistCreateDto
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Des { get; set; }
        public IFormFile Pic { get; set; }
    }
}
