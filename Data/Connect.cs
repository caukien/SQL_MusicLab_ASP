using Microsoft.EntityFrameworkCore;
using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Data
{
    public class Connect : DbContext
    {
        public Connect(DbContextOptions<Connect> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongGenre> SongGenres { get; set; }
        public DbSet<SongPlaylist> SongPlaylists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Song>().Property(s => s.SongId).ValueGeneratedOnAdd();

            modelBuilder.Entity<SongGenre>().HasKey(sg => new { sg.GenreId, sg.SongId });

            modelBuilder.Entity<SongGenre>(e =>
            {
                e.HasOne(s => s.Genre)
                .WithMany(s => s.SongGenres)
                .HasForeignKey(s => s.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(s => s.Song)
                .WithMany(s => s.SongGenres)
                .HasForeignKey(s => s.SongId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SongPlaylist>().HasKey(sp => new { sp.PlaylistId, sp.SongId });

            modelBuilder.Entity<SongPlaylist>(e =>
            {
                e.HasOne(s => s.Playlist)
                .WithMany(s => s.SongPlaylists)
                .HasForeignKey(s => s.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(s => s.Song)
                .WithMany(s => s.SongPlaylists)
                .HasForeignKey(s => s.SongId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Song>()
                .HasOne(s => s.Artist)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.ArtistId);
            //.OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Song>()
                .HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumId);
                //.OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Song>().HasOne(e => e.Publisher)
            //    .WithOne(e => e.Song)
            //    .HasForeignKey<Song>(e => e.SongId);

            //modelBuilder.Entity<Album>().HasMany(e => e.Songs)
            //    .WithOne(e => e.Album)
            //    .HasForeignKey(e => e.AlbumId)
            //    .IsRequired(false);
        }
    }
}
