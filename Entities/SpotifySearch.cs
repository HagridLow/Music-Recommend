using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class SpotifySearch
    {
    public class LinkedFrom
    {
        public Albums album { get; set; }
        public List<Item> artists { get; set; }
        public List<string> available_markets { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool @explicit { get; set; }
        public ExternalIDs external_ids { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public bool is_playable { get; set; }
        public LinkedFrom linked_from { get; set; }
        public Restrictions restrictions { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public bool is_local { get; set; }
    }
        public class ExternalIDs
        {
            public string isrc { get; set; }
            public string ean { get; set;} 
            public string upc { get; set; }
        }

        public class ExternalUrls
        {
            public string spotify { get; set; }
        }

        public class Restrictions
        {
            public string reason { get; set; }
        }

        public class Followers
        {
            public object href { get; set; }
            public int total { get; set; }
        }

        public class ImageSP
        {
            public int width { get; set; }
            public string url { get; set; }
            public int height { get; set; }
        }

        public class Item
        {
            public ExternalUrls external_urls { get; set; }
            public Followers followers { get; set; }
            public List<string> genres { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public List<ImageSP> images { get; set; }
            public string name { get; set; }
            public int popularity { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
        }

        public class TrackItems
        {
            public Albums album { get; set;}
            public List<Item> artists { get; set; }
            public List<string> available_markets { get; set; }
            public int disc_number { get; set; }
            public int duration_ms { get; set; } 
            public bool @explicit { get; set; }
            public ExternalIDs external_ids { get; set; }
            public ExternalUrls external_urls { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public bool is_playable { get; set; }
            public LinkedFrom linked_from { get; set; }
            public Restrictions restrictions { get; set; }
            public string name { get; set; }
            public int popularity { get; set; }
            public string preview_url { get; set; }
            public string type { get; set; }
            public string uri { get; set; }          
            public bool is_local { get; set; }
        }

        public class Tracks
        {
            public string href { get; set; }
            public List<TrackItems> items { get; set; }
            public int limit { get; set; }
            public string next { get; set; }
            public int offset { get; set; }
            public string previous { get; set; }
            public int total { get; set; }
        }

        public class Artists
        {
            public string href { get; set; }
            public List<Item> items { get; set; }
            public int limit { get; set; }
            public string next { get; set; }
            public int offset { get; set; }
            public object previous { get; set; }
            public int total { get; set; }
        }

        public class AlbumItems
        {
            public string album_type { get; set; }
            public int total_tracks { get; set; }
            public List<string> available_markets { get; set; }
            public ExternalUrls external_urls { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public List<ImageSP> images { get; set; }
            public string name { get; set; }
            public string release_date { get; set; }
            public string release_date_precision { get; set; }
            public Restrictions reason { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
            public List<Item> artists { get; set; }
            public List<TrackItems> tracks { get; set; }
        }

        public class Albums
        {
            public string href { get; set; }
            public List<AlbumItems> items { get; set; }
            public string next { get; set; }
            public int offset { get; set; }
            public string previous { get; set; }
            public string total { get; set; }
        }

        public class SpotifyResult
        {
            public Artists artists { get; set; }
            public Albums albums { get; set; }
            public Tracks tracks { get; set; }
        }
    }
}