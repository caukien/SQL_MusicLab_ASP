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
    public class AlbumRepository : IAlbumRepository
    {
        private readonly Connect _context;

        public AlbumRepository(Connect connect)
        {
            _context = connect;
        }

        public async Task<bool> CheckExists(int id)
        {
            return await _context.Albums.AnyAsync(a => a.AlbumId == id);
        }

        public async Task<bool> CheckName(string name)
        {
            var nameTrimed = name.Trim().ToLower();
            var count = await _context.Albums.FirstOrDefaultAsync(a => a.Name == nameTrimed);
            return count != null;
        }

        public async Task<Album> Create(Album album)
        {
            await _context.Albums.AddAsync(album);
            _context.SaveChanges();
            return album;
        }

        public async Task<Album> Delete(int id)
        {
            var item = await _context.Albums.FindAsync(id);
            if (item != null)
            {
                _context.Albums.Remove(item);
                await _context.SaveChangesAsync();
            }
            return item;
        }

        public async Task<Album> Get(int id)
        {
            return await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(a => a.AlbumId == id);
        }

        public async Task<ICollection<Album>> GetAll()
        {
            return await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Songs)
                .ToListAsync();
        }

        public async Task<Album> Update(Album album)
        {
            _context.Albums.Update(album);
            await _context.SaveChangesAsync();
            return album;
        }
    }
}
