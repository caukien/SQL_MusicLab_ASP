using Microsoft.EntityFrameworkCore;
using MusicLab_SQL.Data;
using MusicLab_SQL.Interfaces;
using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Connect _connect;

        public UserRepository(Connect connect)
        {
            _connect = connect;
        }

        public async Task<User> Create(User user)
        {
            await _connect.Users.AddAsync(user);
            _connect.SaveChanges();
            return user;
        }

        public async Task<bool> Exists(int id)
        {
            return await _connect.Users.AnyAsync(u => u.UserId == id);
        }

        public async Task<User> GetuserandPassword(string username, string password)
        {
            var usernameTrimed = username.Trim().ToLower();
            return await _connect.Users.FirstOrDefaultAsync(u => u.Username.Trim().ToLower() == usernameTrimed && u.Password == password);
        }

        public async Task<bool> NameExists(string name)
        {
            var nameTrimed = name.Trim().ToLower();
            var count = await _connect.Users.FirstOrDefaultAsync(u => u.Username.Trim().ToLower() == nameTrimed);
            return count != null;
        }
    }
}
