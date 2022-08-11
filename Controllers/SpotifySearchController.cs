using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using API.Contexts;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swan.Formatters;
using static API.Entities.SpotifySearch;

namespace API.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SpotifySearchController: BaseAPIController
    {
        private readonly SearchHelper _searchHelper;
        private readonly DataContext _dataContext;
        private readonly IUserAccessor _userAccessor;
        private readonly UserManager<AppUser> _userManager;

        public SpotifySearchController(SearchHelper searchHelper, DataContext dataContext, IUserAccessor userAccessor, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _userAccessor = userAccessor;
            _dataContext = dataContext;
            _searchHelper = searchHelper;
        }

        [HttpGet("album/{query}")]
        public async Task<List<SpotifyAlbum>> SearchAlbumMethod(string query)
        {
            await Task.Run(async () => await SearchHelper.GetTokenAsync());

            var result = await SearchHelper.GetAlbumMethod(query);

            var album = new List<SpotifyAlbum>();


            foreach (var item in result.Albums.Items)
            {
                album.Add(new SpotifyAlbum()
                {
                    idAlbum = item.Id,
                    Name = item.Name,
                    Artist = item.Artists.Any() ? item.Artists[0].Name : "e pa nema",
                    Image = item.Images.Any() ? item.Images[0].Url : "https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png",
                    TotalTracks = item.TotalTracks,
                    ReleaseDate = item.ReleaseDate,
                    totRating = _dataContext.SpotifyAlbumRateds.Where(x => x.idAlbum == item.Id).ToList().Select(z => z.Rating).DefaultIfEmpty(0).Average()
                });
            }

            return album;
        }


        [Cached(600)]
        [HttpPost("album/{id}")]
        public async Task<ActionResult<SpotifyAlbumRated>> RateAlbum(string id, SpotifyAlbumRated spotifyAlbum)
        {

            await Task.Run(async () => await SearchHelper.GetTokenAsync());

            var result = await SearchHelper.GetAlbumById(id);

            var item = result;


            var userID = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var user = await _dataContext.Users.FindAsync(userID.Id);
            if (user == null) {
                return BadRequest("User not found");
            }

            

            spotifyAlbum.ID = Guid.NewGuid().ToString();
            spotifyAlbum.idAlbum = item.Id;
            spotifyAlbum.Name = item.Name;
            spotifyAlbum.Artist = item.Artists.Any() ? item.Artists[0].Name : "";
            spotifyAlbum.Image = item.Images.Any() ? item.Images[0].Url : "https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png";
            spotifyAlbum.TotalTracks = item.TotalTracks;
            spotifyAlbum.ReleaseDate = item.ReleaseDate;
            spotifyAlbum.totRating = _dataContext.SpotifyAlbumRateds.Where(x => x.idAlbum == item.Id).ToList().Select(z => z.Rating).DefaultIfEmpty(0).Average();

            if (user.SpotifyAlbumRateds.Where(c => c.idAlbum == id).ToList().Select(z => z.idAlbum).FirstOrDefault() == item.Id) return BadRequest("Can't rate an already rated album");


            user.SpotifyAlbumRateds.Add(spotifyAlbum);
            _dataContext.SpotifyAlbumRateds.Add(spotifyAlbum);
            await _userManager.UpdateAsync(user);
            await _dataContext.SaveChangesAsync();

            return spotifyAlbum;

        }


        [HttpDelete("deletealbums")]
        public async Task<IQueryable<SpotifyAlbumRated>> WipeRatedAlbums() //just for development
        {
            var albums = _dataContext.SpotifyAlbumRateds;

            foreach(var album in albums)
            {
                _dataContext.SpotifyAlbumRateds.Remove(album);
            }

            await _dataContext.SaveChangesAsync();

            return albums;
        }
    }
}
