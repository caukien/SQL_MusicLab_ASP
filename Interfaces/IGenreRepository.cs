using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Interfaces
{
    public interface IGenreRepository
    {
        Task<ICollection<Genre>> GetAll();
        Task<Genre> Get(int id);
        Task<Genre> Create(Genre genre);
        Task<Genre> Update(Genre genre);
        Task<Genre> Delete(int id);
        Task<bool> CheckExists(int id);
        Task<bool> CheckName(string name);
    }
}
