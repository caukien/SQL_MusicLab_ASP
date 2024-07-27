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
    public class GenreRepository : IGenreRepository
    {
        private readonly Connect _context;

        public GenreRepository(Connect connect)
        {
            _context = connect;
        }

        public async Task<bool> CheckExists(int id)
        {
            return await _context.Genres.AnyAsync(g => g.GenreId == id);
        }

        public async Task<bool> CheckName(string name)
        {
            var nametrimed = name.Trim().ToLower();
            var count = await _context.Genres.FirstOrDefaultAsync(c => c.Name.Trim().ToLower() == nametrimed);
            return count != null;
        }

        public async Task<Genre> Create(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> Delete(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
            return genre;
        }

        public async Task<Genre> Get(int id)
        {
            return await _context.Genres.Include(g => g.SongGenres).ThenInclude(g => g.Song).FirstOrDefaultAsync(g => g.GenreId == id);
        }

        public async Task<ICollection<Genre>> GetAll()
        {
            return await _context.Genres.Include(g => g.SongGenres).ThenInclude(g => g.Song).ToListAsync();
        }

        public async Task<Genre> Update(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
    }
}
