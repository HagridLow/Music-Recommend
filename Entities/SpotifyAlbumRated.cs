using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class SpotifyAlbumRated: SpotifyAlbum
    {
        public int Rating { get; set; }
        public int AlbumStatusId { get; set; }
        public AlbumStatus AlbumStatus { get; set; }
        public ICollection<AlbumRaters> Raters { get; set; } = new List<AlbumRaters>();
    }
}