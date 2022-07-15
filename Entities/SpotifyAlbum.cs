using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static API.Entities.SpotifySearch;

namespace API.Entities
{
    public class SpotifyAlbum
    {
        public string idAlbum { get; set; }
        public string Name { get; set; }
        public string Artist { get; set;}
        public string Image { get; set; }
        public int TotalTracks { get; set; }
        public string ReleaseDate { get; set; }
        public double? sumRating { get; set; }

    }
}