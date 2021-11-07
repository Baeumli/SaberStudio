using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class MapDetailMetadata
    {
        [JsonProperty("bpm")]
        public float Bpm { get; set; }
        
        [JsonProperty("duration")]
        public int Duration { get; set; }
        
        [JsonProperty("levelAuthorName")]
        public string MapAuthor { get; set; }
        
        [JsonProperty("songAuthorName")]
        public string SongAuthor { get; set; }
        
        [JsonProperty("songName")]
        public string SongName { get; set; }
        
        [JsonProperty("songSubName")]
        public string SongSubName { get; set; }
    }
}