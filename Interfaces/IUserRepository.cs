using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Exists(int id);
        Task<bool> NameExists(string name);
        Task<User> Create(User user);
        Task<User> GetuserandPassword(string username, string password);
    }
}
