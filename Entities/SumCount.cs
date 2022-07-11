using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class SumCount
    {
        public SpotifyAlbumRated SpotifyAlbumRated { get; set; }
        public SpotifyAlbum SpotifyAlbum { get; set;}
        public int sumCount { get; set; }
    }
}