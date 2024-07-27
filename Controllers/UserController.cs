using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicLab_SQL.Dto.User;
using MusicLab_SQL.Interfaces;
using MusicLab_SQL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicLab_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);
            if (await _userRepository.NameExists(userDto.Username))
                return BadRequest($"User with name '{userDto.Username}' already exsits");
            var userMap = _mapper.Map<User>(userDto);
            await _userRepository.Create(userMap);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(userMap);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);
            var user = await _userRepository.GetuserandPassword(userDto.Username, userDto.Password);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound();
        }
        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issure"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
