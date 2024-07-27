using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLab_SQL.Dto.Album;
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
    public class AlbumController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public AlbumController(IAlbumRepository albumRepository, IMapper mapper, ISongRepository songRepository)
        {
            _songRepository = songRepository;
            _albumRepository = albumRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = _mapper.Map<List<AlbumDto>>(await _albumRepository.GetAll());
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = _mapper.Map<AlbumDto>(await _albumRepository.Get(id));
            //var item = await _albumRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AlbumCreateDto albumCreateDto)
        {
            if (albumCreateDto == null)
                return BadRequest(ModelState);

            var checkname = await _albumRepository.CheckName(albumCreateDto.Name);
            if (checkname)
                return Conflict("Name already exists.");

            var map = _mapper.Map<Album>(albumCreateDto);
            map.Songs = new List<Song>();
            foreach (var songId in albumCreateDto.SongIds)
            {
                var song = await _songRepository.Get(songId);
                if (song != null)
                {
                    map.Songs.Add(song);
                }
            }
            if (await _albumRepository.Create(map) == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok(map);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AlbumCreateDto albumCreateDto)
        {
            if (albumCreateDto == null)
                return BadRequest(ModelState);

            if (!await _albumRepository.CheckExists(id))
                return NotFound();


            var updateitem = _mapper.Map(albumCreateDto, await _albumRepository.Get(id));

            if (await _albumRepository.Update(updateitem) == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok(updateitem);

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!await _albumRepository.CheckExists(id))
                return NotFound();

            var deleteitem = await _albumRepository.Delete(id);

            if (deleteitem == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
