using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Interfaces
{
    public interface IArtistRepository
    {
        Task<ICollection<Artist>> GetAll();
        Task<Artist> Get(int id);
        Task<Artist> Create(Artist artist);
        Task<Artist> Update(Artist artist);
        Task<Artist> Delete(int id);
        Task<bool> CheckExists(int id);
        Task<bool> CheckName(string name);
    }
}
