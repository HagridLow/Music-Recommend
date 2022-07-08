using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Profile
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public bool Following { get; set; }
        public int FollowingCount { get; set; }
        public int FollowersCount { get; set; }
        public List<SpotifyAlbumRated> SpotifyAlbumRateds{ get; set; }
    }
}