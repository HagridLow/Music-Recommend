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



        [HttpPost("ratealbum")]
        public async Task<ActionResult<SpotifyAlbumRated>> RateAlbum(string search, SpotifyAlbumRated spotifyAlbum)
        {    

            await Task.Run(async () => await SearchHelper.GetTokenAsync());

            var result = SearchHelper.GetAlbumTrackOrArtist(search);

            var item = result.albums.items;



            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if (user == null){
                return BadRequest("User not found");
            }     

            spotifyAlbum.ID = Guid.NewGuid().ToString();
            spotifyAlbum.idAlbum = item.Any() ? item[0].id : "";
            spotifyAlbum.Name = item.Any() ? item[0].name : "";
            spotifyAlbum.Artist = item.Any() ? item[0].artists.Any() ? item[0].artists[0].name : "" : "";
            spotifyAlbum.Image = item.Any() ? item[0].images.Any()? item[0].images[0].url : "" : "https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png";
            spotifyAlbum.TotalTracks = item.Any() ? item[0].total_tracks : 0;
            spotifyAlbum.ReleaseDate = item.Any() ? item[0].release_date : "";


            user.SpotifyAlbumRateds.Add(spotifyAlbum);
            _dataContext.SpotifyAlbumRateds.Add(spotifyAlbum);
            await _dataContext.SaveChangesAsync();
            return spotifyAlbum;

        }


    }
}