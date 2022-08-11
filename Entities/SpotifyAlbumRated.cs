using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Entities
{
    public class SpotifyAlbumRated : SpotifyAlbum
    {
        public string ID { get; set; }
        public int Rating { get; set; }
        [JsonIgnore]
        public string  AppUserId { get ; set; }
        [JsonIgnore]
        public AppUser User { get; set; }
    }
}