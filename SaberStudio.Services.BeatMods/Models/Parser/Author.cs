using Newtonsoft.Json;
using System;

namespace SaberStudio.Services.BeatMods.Models.Parser
{
    public class Author
    {
        [JsonProperty("lastLogin")]
        public DateTime LastLogin { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}
