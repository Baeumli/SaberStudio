using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class UserDiffStats
    {
        [JsonProperty("easy")]
        public int Easy { get; set; }
        
        [JsonProperty("normal")]
        public int Normal { get; set; }
        
        [JsonProperty("hard")]
        public int Hard { get; set; }
        
        [JsonProperty("expert")]
        public int Expert { get; set; }
        
        [JsonProperty("expertPlus")]
        public int ExpertPlus { get; set; }
        
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}