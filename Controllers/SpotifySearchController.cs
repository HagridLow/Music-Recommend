using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swan.Formatters;
using static API.Entities.SpotifySearch;

namespace API.Controllers
{
    [ApiController]
    [Route("api/spotifysearch")]
    public class SpotifySearchController: BaseAPIController
    {
        private readonly SearchHelper _searchHelper;
        private readonly DataContext _dataContext;

        public SpotifySearchController(SearchHelper searchHelper, DataContext dataContext)
        {
            _dataContext = dataContext;
            _searchHelper = searchHelper;
        }

        [HttpPost("album")]
        public async Task<ActionResult<SpotifyAlbum>> SearchAlbums(string search)
        {    

            await Task.Run(async () => await SearchHelper.GetTokenAsync());
            
            var result = SearchHelper.GetAlbumTrackOrArtist(search);

            var album = new SpotifyAlbum();
            
            foreach(var item in result.albums.items)
            {
 
                album.ID = item.id;
                album.Name = item.name;
                album.Artist = item.artists.Any() ? item.artists[0].name : "e pa nema";
                album.Image = item.images.Any() ? item.images[0].url : "https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png";
                album.TotalTracks = item.total_tracks;
                album.ReleaseDate = item.release_date;
                album.AlbumRatingId = 1;
                album.AlbumStatusId = 2;


                _dataContext.SpotifyAlbums.Add(album);
                await _dataContext.SaveChangesAsync();
                return album;

            }    



            return album;

        }

        [HttpGet("albums")]
        public async Task<IReadOnlyList<SpotifyAlbum>> GetSpotifyAlbumAsync()
        {
            var album = await _dataContext.SpotifyAlbums.ToListAsync();

            return album;
        }   
    }
}