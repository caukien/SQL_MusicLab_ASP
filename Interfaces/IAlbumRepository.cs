using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Interfaces
{
    public interface IAlbumRepository
    {
        Task<ICollection<Album>> GetAll();
        Task<Album> Get(int id);
        Task<Album> Create(Album album);
        Task<Album> Update(Album album);
        Task<Album> Delete(int id);
        Task<bool> CheckExists(int id);
        Task<bool> CheckName(string name);
    }
}
