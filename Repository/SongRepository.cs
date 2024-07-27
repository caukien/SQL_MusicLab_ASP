using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicLab_SQL.Data;
using MusicLab_SQL.Dto.Song;
using MusicLab_SQL.Interfaces;
using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly Connect _context;
        private readonly IMapper _mapper;

        public SongRepository(Connect connect, IMapper mapper)
        {
            _context = connect;
            _mapper = mapper;
        }

        public async Task<bool> CheckExists(int id)
        {
            return await _context.Songs.AnyAsync(g => g.SongId == id);
        }

        public async Task<bool> CheckName(string name)
        {
            var nametrimed = name.Trim().ToLower();
            var count = await _context.Songs.FirstOrDefaultAsync(c => c.Name.Trim().ToLower() == nametrimed);
            return count != null;
        }

        public async Task<bool> Create(Song song)
        {
            await _context.Songs.AddAsync(song);
            return await Save();
        }

        public async Task<bool> Delete(int id)
        {
            var genre = await _context.Songs.FindAsync(id);
            if (genre != null)
            {
                _context.Songs.Remove(genre);
                await Save();
            }
            return false;
        }

        public async Task<Song> Get(int id)
        {
            return await _context.Songs.Include(s => s.SongGenres)
                .ThenInclude(s => s.Genre)
                .FirstOrDefaultAsync(s=>s.SongId == id);
        }

        public async Task<ICollection<Song>> GetAll()
        {
            return await _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .Include(s => s.SongGenres)
                    .ThenInclude(s => s.Genre)
                .ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Song song)
        {
            _context.Songs.Update(song);
            return await Save();
        }
    }
}
