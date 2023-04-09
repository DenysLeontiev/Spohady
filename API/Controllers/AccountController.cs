using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManger;
        private readonly ITokenHandler _tokenHandler;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManger = signInManager;
            _tokenHandler = tokenHandler;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email))
            {
                return BadRequest("User already exists");
            }

            AppUser userToCreate = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(userToCreate, registerDto.Password);

            if (result.Succeeded)
            {
                UserDto userDto = new UserDto 
                {
                    Username = userToCreate.UserName,
                    Email = userToCreate.Email,
                    Token = _tokenHandler.CreateToken(userToCreate),
                };

                return Ok(userDto);
            }

            return BadRequest("An error during registering occured");
            // return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null)
            {
                return NotFound($"User with name {loginDto.Username} is not found");
            }

            var result = await _signInManger.PasswordSignInAsync(user, loginDto.Password, false, false);

            if (result.Succeeded)
            {

                UserDto userDto = new UserDto 
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _tokenHandler.CreateToken(user),
                };

                return Ok(userDto);
            }

            return BadRequest();
        }

        private async Task<bool> UserExists(string userEmail)
        {
            if (await _userManager.FindByEmailAsync(userEmail) == null)
            {
                return false;
            }

            return true;
        }
    }
}