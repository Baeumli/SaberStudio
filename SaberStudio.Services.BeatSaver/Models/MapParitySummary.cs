using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class MapParitySummary
    {
        [JsonProperty("errors")]
        public int Errors { get; set; }
        
        [JsonProperty("resets")]
        public int Resets { get; set; }
        
        [JsonProperty("warns")]
        public int Warnings { get; set; }
    }
}