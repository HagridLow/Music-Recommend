using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class SpotifyTrack: BaseEntity
    {
        public string Name { get; set; }
        public string Artist { get; set;}
        public int Duration { get; set; }
    }
}