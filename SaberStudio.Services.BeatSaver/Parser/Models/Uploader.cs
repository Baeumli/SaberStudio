using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Parser.Models
{
    public class Uploader
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}
