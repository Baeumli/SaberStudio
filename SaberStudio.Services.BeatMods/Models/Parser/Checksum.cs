using Newtonsoft.Json;

namespace SaberStudio.Services.BeatMods.Models.Parser
{
    public class Checksum
    {
        [JsonProperty("file")]
        public string File { get; set; }
        [JsonProperty("hash")]
        public string Md5Hash { get; set; }
    }
}
