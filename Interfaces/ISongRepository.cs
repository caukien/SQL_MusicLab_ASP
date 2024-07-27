using MusicLab_SQL.Dto.Song;
using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Interfaces
{
    public interface ISongRepository
    {
        Task<ICollection<Song>> GetAll();
        Task<Song> Get(int id);
        Task<bool> Create(Song song);
        Task<bool> Update(Song song);
        Task<bool> Delete(int id);
        Task<bool> Save();
        Task<bool> CheckExists(int id);
        Task<bool> CheckName(string name);
    }
}
