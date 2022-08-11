using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : BaseAPIController
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProfilesController(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("search/{username}")]
        public async Task<ActionResult<List<Profile>>> SearchUsers(string username)
        {
            var users = await _userManager.Users.Include(a => a.SpotifyAlbumRateds).Where(x => x.UserName!.Contains(username)).ToListAsync();

            var userList = new List<Profile>();

            foreach (var user in users)
            {
                userList.Add(new Profile()
                {
                    Username = user.UserName,
                    Bio = user.Bio,
                    Image = "../assets/images/stockprofileimage.jpg",
                    SpotifyAlbumRateds = user.SpotifyAlbumRateds
                });
            }

            return userList;
        }

        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Profile>> GetUser(string targetUsername)
        {
            var user = await _context.Users
            .Include(a => a.SpotifyAlbumRateds)
            .SingleOrDefaultAsync(x => x.UserName == targetUsername);
            
            var profile = new Profile();

            profile.Username = user.UserName;
            profile.Bio = user.Bio;
            profile.Image = "../assets/images/stockprofileimage.jpg";
            profile.SpotifyAlbumRateds = user.SpotifyAlbumRateds.ToList();

            return profile;
        }
    }
}