using Newtonsoft.Json;
using System.Collections.Generic;

namespace SaberStudio.Services.BeatSaber.Parser.Models
{
    public class Playlist
    {
        [JsonProperty("playlistTitle")]
        public string PlaylistTitle { get; set; }

        [JsonProperty("playlistAuthor")]
        public string PlaylistAuthor { get; set; }

        [JsonProperty("playlistDescription")]
        public string PlaylistDescription { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("songs")]
        public IEnumerable<Song> Songs { get; set; }

        public class Song
        {
            [JsonProperty("hash")]
            public string SongHash { get; set; }
        }
    }
}
