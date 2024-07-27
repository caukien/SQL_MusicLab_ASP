using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLab_SQL.Dto.Artist;
using MusicLab_SQL.Interfaces;
using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLab_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;
        private readonly UploadService _uploadService;

        private bool ValidateFileType(IFormFile file)
        {
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            return Array.Exists(allowedTypes, type => type == file.ContentType);
        }

        public ArtistController(IArtistRepository artistRepository, IMapper mapper, UploadService uploadService)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
            _uploadService = uploadService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = _mapper.Map<List<ArtistDto>>(await _artistRepository.GetAll());
            //var items = await _artistRepository.GetAll();
            return Ok(items);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var item = _mapper.Map<ArtistDto>(await _artistRepository.Get(id));
            //var item = await _artistRepository.Get(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ArtistCreateDto artistCreateDto)
        {
            //if (artistCreateDto == null)
            //    return BadRequest(ModelState);

            //var checkname = await _artistRepository.CheckName(artistCreateDto.Name);
            //if (checkname)
            //    return Conflict("Name already exists.");

            //var map = _mapper.Map<Artist>(artistCreateDto);
            //if (await _artistRepository.Create(map) == null)
            //{
            //    ModelState.AddModelError("", "Something went wrong");
            //    return StatusCode(500, ModelState);
            //}
            //return Ok(map);

            if (artistCreateDto == null)
                return BadRequest(ModelState);

            if (!ValidateFileType(artistCreateDto.Pic))
                return BadRequest("Invalid file type. Only JPG, PNG, and WEBP files are allowed.");

            var checkname = await _artistRepository.CheckName(artistCreateDto.Name);
            if (checkname)
                return Conflict("Name already exists.");

            string filePath = null;

            if (artistCreateDto.Pic != null && artistCreateDto.Pic.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await artistCreateDto.Pic.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    var fileContent = Convert.ToBase64String(fileBytes);

                    filePath = await _uploadService.UploadFileAsync(
                        artistCreateDto.Pic.FileName,
                        fileContent,
                        artistCreateDto.Pic.ContentType
                    );
                }
            }

            var artist = _mapper.Map<Artist>(artistCreateDto);
            artist.Pic = filePath;

            if (await _artistRepository.Create(artist) == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok(artist);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] ArtistCreateDto artistCreateDto)
        {
            //if (artistCreateDto == null)
            //    return BadRequest(ModelState);

            //if (!await _artistRepository.CheckExists(id))
            //    return NotFound();

            //var updateitem = _mapper.Map(artistCreateDto, await _artistRepository.Get(id));

            //if (await _artistRepository.Update(updateitem) == null)
            //{
            //    ModelState.AddModelError("", "Something went wrong");
            //    return StatusCode(500, ModelState);
            //}
            //return Ok(updateitem);

            if (artistCreateDto == null)
                return BadRequest(ModelState);

            if (!await _artistRepository.CheckExists(id))
                return NotFound();

            if (!ValidateFileType(artistCreateDto.Pic))
                return BadRequest("Invalid file type. Only JPG, PNG, and WEBP files are allowed.");

            var artistToUpdate = await _artistRepository.Get(id);
            if (artistToUpdate == null)
                return NotFound();

            string filePath = artistToUpdate.Pic;

            if (artistCreateDto.Pic != null && artistCreateDto.Pic.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await artistCreateDto.Pic.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    var fileContent = Convert.ToBase64String(fileBytes);

                    filePath = await _uploadService.UploadFileAsync(
                        artistCreateDto.Pic.FileName,
                        fileContent,
                        artistCreateDto.Pic.ContentType
                    );
                }
            }

            var artist = _mapper.Map(artistCreateDto, artistToUpdate);
            artist.Pic = filePath;

            if (await _artistRepository.Update(artist) == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok(artist);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!await _artistRepository.CheckExists(id))
                return NotFound();

            var deleteitem = await _artistRepository.Delete(id);

            if (deleteitem == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
