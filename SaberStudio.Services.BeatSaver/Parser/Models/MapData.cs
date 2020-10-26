using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Parser.Models
{
    public class MapData
    {
        [JsonProperty("bombs")]
        public int Bombs { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("length")]
        public double Length { get; set; }

        [JsonProperty("njs")]
        public double Njs { get; set; }

        [JsonProperty("njsOffset")]
        public double NjsOffset { get; set; }

        [JsonProperty("notes")]
        public int Notes { get; set; }

        [JsonProperty("obstacles")]
        public int Obstacles { get; set; }
    }
}
