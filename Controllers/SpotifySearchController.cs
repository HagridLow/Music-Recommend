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
        
        [Cached(600)]
        [HttpGet("getalbum")]
        public async Task<List<SpotifyAlbum>> SearchAlbums(string search)
        {
            await Task.Run(async () => await SearchHelper.GetTokenAsync());

            var result = SearchHelper.GetAlbumTrackOrArtist(search);

            var album = new List<SpotifyAlbum>();
            
            foreach(var item in result.albums.items)
            {
                
                album.Add(new SpotifyAlbum(){
                   idAlbum = item.id,
                   Name = item.name,
                   Artist = item.artists.Any() ? item.artists[0].name : "e pa nema",
                   Image = item.images.Any() ? item.images[0].url: "https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png",
                   TotalTracks = item.total_tracks,
                   ReleaseDate = item.release_date,
                   sumRating = _dataContext.SpotifyAlbumRateds.Where(x => x.idAlbum == item.id).ToList().Select(z => z.Rating).DefaultIfEmpty(0).Average()
                }); 
            }    

            return album;
            
        }


        [Cached(600)]
        [HttpPost("ratealbum/{id}")]
        public async Task<ActionResult<SpotifyAlbumRated>> RateAlbum(string id, SpotifyAlbumRated spotifyAlbum)
        {    

            await Task.Run(async () => await SearchHelper.GetTokenAsync());

            var result = await SearchHelper.GetAlbumById(id);

            var item = result;

            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if (user == null){
                return BadRequest("User not found");
            }     

            spotifyAlbum.ID = Guid.NewGuid().ToString();
            spotifyAlbum.idAlbum = item.Id;
            spotifyAlbum.Name = item.Name;
            spotifyAlbum.Artist = item.Artists.Any() ? item.Artists[0].Name : "";
            spotifyAlbum.Image = item.Images.Any() ? item.Images[0].Url : "https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png";
            spotifyAlbum.TotalTracks = item.TotalTracks;
            spotifyAlbum.ReleaseDate = item.ReleaseDate;

            var albumInAlbumRateds = user.SpotifyAlbumRateds.Where(x => x.idAlbum == item.Id).ToString();
            if(albumInAlbumRateds == item.Id)
            {
                return BadRequest("Cannot rate an already rated album");
            }

            user.SpotifyAlbumRateds.Add(spotifyAlbum);
            _dataContext.SpotifyAlbumRateds.Add(spotifyAlbum);
            await _dataContext.SaveChangesAsync();
            return spotifyAlbum;

        }


    }
}