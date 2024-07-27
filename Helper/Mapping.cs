using AutoMapper;
using MusicLab_SQL.Dto;
using MusicLab_SQL.Dto.Album;
using MusicLab_SQL.Dto.Artist;
using MusicLab_SQL.Dto.Genre;
using MusicLab_SQL.Dto.Song;
using MusicLab_SQL.Dto.User;
using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            //Genre
            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.Song, opt => opt.MapFrom(src => src.SongGenres.Select(sg => sg.Song).ToList()));
            CreateMap<Genre, GenreCreateDto>().ReverseMap();

            //Song
            CreateMap<Song, SongDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.SongGenres.Select(sg => sg.Genre).ToList()))
                //.ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
                .ReverseMap();
            CreateMap<SongCreateDto, Song>()
                //.ForMember(dest => dest.SongGenres, opt => opt.MapFrom(src => src.GenreIds.Select(id => new SongGenre { GenreId = id })));
                .ForMember(dest => dest.SongGenres, opt => opt.MapFrom(src => src.SongGenres.Select(genreId => new SongGenre { GenreId = genreId })));

            //Artist
            CreateMap<Artist, ArtistDto>()
                .ReverseMap();
            CreateMap<Artist, ArtistCreateDto>().ReverseMap();

            //Album
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<AlbumCreateDto, Album>()
                 .ForMember(dest => dest.Songs, opt => opt.Ignore());
            //.ForMember(dest => dest.Artist, opt => opt.Ignore()) // Bỏ qua ánh xạ cho Artist
            //.ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src => src.Artist)); // Chỉ ánh xạ ArtistId


            CreateMap<SongGenre, SongGenreDto>().ReverseMap();


            //User
            CreateMap<User, UserDto>().ReverseMap();
            //Simple Dto
            CreateMap<Artist, ArtistSimpleDto>();
            CreateMap<Song, SongSimpleDto>();
        }
    }
}
