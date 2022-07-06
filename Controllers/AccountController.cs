using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Photos;
using API.Security;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService, IEmailService emailService, DataContext context, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _context = context;
            _emailService = emailService;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            
            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Email taken");
            }
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                return BadRequest("Username taken");
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);


            if(result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var link = Url.Action(nameof(VerifyEmail), "account", new { userId = user.Id, code}, Request.Scheme, Request.Host.ToString());

                await _emailService.SendAsync(user.Email, "Email Verify:", $"<a href=\"{link}\">Verify Email</a>", true);

                Console.WriteLine("Email Verification Sent");

                return Ok("Email Verification Send");
            }


            return BadRequest("Problem Registering User");
        }


        public async Task<ActionResult<UserDto>> VerifyEmail (string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("User non existent");
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }
            
            return BadRequest("Problem Verifying Email");
        }


        [HttpGet(("profiles"))]
        public async Task<ActionResult<Profile>> GetUser(string targetUsername)
        {
            var user = await _context.Users
            .Include(a => a.SpotifyAlbumRateds)
            .SingleOrDefaultAsync(x => x.UserName == targetUsername);
            
            var profile = new Profile();

            profile.Username = user.UserName;
            profile.Bio = user.Bio;
            profile.Image = "../assets/images/stockprofileimage.jpg";
            profile.SpotifyAlbumRateds = user.SpotifyAlbumRateds;

            return profile;
        }

        [HttpPost("photo")]
        public async Task<IActionResult> AddPhoto([FromForm] AddPhotoHandler.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("photo/{id}")]
        public async Task<IActionResult> DeletePhoto(string id)
        {
            return HandleResult(await Mediator.Send(new DeletePhotoHandler.Command{Id = id}));
        }

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Image = "../assets/images/stockprofileimage.jpg",
                Token = _tokenService.CreateToken(user),
                Username = user.UserName,
            };
        }

    }
}