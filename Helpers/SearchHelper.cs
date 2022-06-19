using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SpotifyAPI.Web;
using static API.Entities.SpotifySearch;


namespace API.Helpers
{
    public class SearchHelper
    {
        public static string Token { get; set; }
        public static async Task GetTokenAsync()
        {
            #region SecretVault 
            var CLIENT_ID = "359dc7663c474c4380592faf66b6ab2a";
            var CLIENT_SECRET = "94a4a4a152e04b2a92b9fc46103aa50f";
            #endregion
            
            //creating 
            var config = SpotifyClientConfig.CreateDefault();

            var request = new ClientCredentialsRequest(CLIENT_ID, CLIENT_SECRET);
            var response = await new OAuthClient(config).RequestToken(request);

            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

            Token = response.AccessToken;
        }
        public static SpotifyResult GetAlbumTrackOrArtist(string search)
        {
            var client = new RestClient("https://api.spotify.com/v1/search");
            client.AddDefaultHeader("Authorization", $"Bearer {Token}");
            var request = new RestRequest($"?q={search}&type=album", Method.Get);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<SpotifyResult>(response.Content); 

            return result;
        }
    }
}