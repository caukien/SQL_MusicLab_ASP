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
    public class ArtistRepository : IArtistRepository
    {
        private readonly Connect _context;

        public ArtistRepository(Connect connect)
        {
            _context = connect;
        }

        public async Task<bool> CheckExists(int id)
        {
            return await _context.Artists.AnyAsync(a => a.ArtistId == id);
        }

        public async Task<bool> CheckName(string name)
        {
            var nametrimed = name.Trim().ToLower();
            var count = await _context.Artists.FirstOrDefaultAsync(c => c.Name.Trim().ToLower() == nametrimed);
            return count != null;
        }

        public async Task<Artist> Create(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
            return artist;
        }

        public async Task<Artist> Delete(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();
            }
            return artist;
        }

        public async Task<Artist> Get(int id)
        {
            return await _context.Artists.Include(a => a.Songs).Include(a => a.Albums)
                .FirstOrDefaultAsync(a => a.ArtistId == id);
        }

        public async Task<ICollection<Artist>> GetAll() => await _context.Artists.Include(a => a.Songs).Include(a => a.Albums).ToListAsync();

        public async Task<Artist> Update(Artist artist)
        {
            _context.Artists.Update(artist);
            await _context.SaveChangesAsync();
            return artist;
        }
    }
}
