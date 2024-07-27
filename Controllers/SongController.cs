using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLab_SQL.Dto;
using MusicLab_SQL.Dto.Song;
using MusicLab_SQL.Interfaces;
using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public SongController(ISongRepository songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = _mapper.Map<List<SongDto>>(await _songRepository.GetAll());
            //var items = await _songRepository.GetAll();
            return Ok(items);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var item = await _songRepository.Get(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SongCreateDto songCreateDto)
        {
            if (songCreateDto == null)
                return BadRequest(ModelState);

            var checkname = await _songRepository.CheckName(songCreateDto.Name);
            if (checkname)
                return Conflict("Name already exists.");

            var song = _mapper.Map<Song>(songCreateDto);

            await _songRepository.Create(song);
            return Ok(song);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]SongCreateDto song)
        {
            if (song == null)
                return BadRequest(ModelState);


            if (!await _songRepository.CheckExists(id))
                return NotFound();

            var existingSong = await _songRepository.Get(id);
            if (existingSong == null)
                return NotFound();

            _mapper.Map(song, existingSong);

            // Xoá các thể loại cũ và thêm các thể loại mới
            existingSong.SongGenres.Clear();
            if (song.SongGenres != null)
            {
                existingSong.SongGenres = song.SongGenres.Select(genreId => new SongGenre { GenreId = genreId }).ToList();
            }

            if (!await _songRepository.Update(existingSong))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok(song);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!await _songRepository.CheckExists(id))
                return NotFound();

            if (!await _songRepository.Delete(id))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
