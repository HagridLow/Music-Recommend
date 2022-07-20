using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController: BaseAPIController
    {
        private readonly DataContext _context;
        public ProfilesController(DataContext context)
        {
            _context = context;
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
            profile.SpotifyAlbumRateds = user.SpotifyAlbumRateds;

            return profile;
        }
    }
}