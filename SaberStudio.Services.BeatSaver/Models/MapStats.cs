using System.Text.Json;
using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class MapStats
    {
        [JsonProperty("downloads")]
        public int Downloads { get; set; }
        
        [JsonProperty("downvotes")]
        public int DownVotes { get; set; }
        
        [JsonProperty("plays")]
        public int Plays { get; set; }
        
        [JsonProperty("score")]
        public float Score { get; set; }
        
        [JsonProperty("upvotes")]
        public int UpVotes { get; set; }
    }
}