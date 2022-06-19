using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class SpotifyArtist: BaseEntity
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public List<string> Genres { get; set; }
    }
}