using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLab_SQL.Dto;
using MusicLab_SQL.Dto.Genre;
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
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]GenreCreateDto genreCreateDto)
        {
            if (genreCreateDto == null)
                return BadRequest(ModelState);

            var checkname = await _genreRepository.CheckName(genreCreateDto.Name);
            if(checkname)
                return Conflict("Name already exists.");
            var map = _mapper.Map<Genre>(genreCreateDto);
            if (await _genreRepository.Create(map) == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok(map);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = _mapper.Map<List<GenreDto>>(await _genreRepository.GetAll());
            //var items = await _genreRepository.GetAll();
            return Ok(items);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var item = _mapper.Map<GenreDto>(await _genreRepository.Get(id));
            //var item = await _genreRepository.Get(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]GenreCreateDto genreDto)
        {
            if (genreDto == null)
                return BadRequest(ModelState);


            if(!await _genreRepository.CheckExists(id))
                return NotFound();

            var updateitem = _mapper.Map(genreDto, await _genreRepository.Get(id));

            if(await _genreRepository.Update(updateitem) == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok(updateitem);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!await _genreRepository.CheckExists(id))
                return NotFound();

            var updateitem = await _genreRepository.Delete(id);

            if (updateitem == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
