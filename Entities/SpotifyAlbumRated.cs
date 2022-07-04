using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Entities
{
    public class SpotifyAlbumRated : SpotifyAlbum
    {
        public int Rating { get; set; }
        public string  AppUserId { get ; set; }
        [JsonIgnore]
        public virtual AppUser User { get; set; }
    }
}