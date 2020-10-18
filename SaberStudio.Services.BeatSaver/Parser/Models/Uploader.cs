using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Parser.Models
{
    public class Uploader
    {
        [JsonProperty("difficulties")]
        public Difficulty Difficulties { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("songName")]
        public string SongName { get; set; }

        [JsonProperty("songSubName")]
        public string SongSubName { get; set; }

        [JsonProperty("songAuthorName")]
        public string SongAuthor { get; set; }

        [JsonProperty("songLevelName")]
        public string LevelAuthor { get; set; }

        [JsonProperty("bpm")]
        public string Bpm { get; set; }
    }
}
