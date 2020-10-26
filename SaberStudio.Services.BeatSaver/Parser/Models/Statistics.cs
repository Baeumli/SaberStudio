using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Parser.Models
{
    public class Statistics
    {
        [JsonProperty("downVotes")]
        public int Downvotes { get; set; }

        [JsonProperty("downloads")]
        public int Downloads { get; set; }

        [JsonProperty("heat")]
        public double Heat { get; set; }

        [JsonProperty("plays")]
        public int Plays { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("upVotes")]
        public int Upvotes { get; set; }
    }
}
