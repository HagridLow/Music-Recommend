using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AlbumRaters
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string AlbumId { get; set; }
        public SpotifyAlbumRated SpotifyAlbumRated { get; set; }
    }
}