using Newtonsoft.Json;
using System.Collections.Generic;

namespace SaberStudio.Services.BeatSaver.Parser.Models
{
    public class Metadata
    {
        [JsonProperty("bpm")]
        public string Bpm { get; set; }

        [JsonProperty("characteristics")]
        public IEnumerable<Mode> Characteristics { get; set; }

        [JsonProperty("difficulties")]
        public Difficulty Difficulties { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("levelAuthorName")]
        public string LevelAuthor { get; set; }

        [JsonProperty("songAuthorName")]
        public string SongAuthor { get; set; }

        [JsonProperty("songName")]
        public string SongName { get; set; }

        [JsonProperty("songSubName")]
        public string SongSubName { get; set; }
    }
}
