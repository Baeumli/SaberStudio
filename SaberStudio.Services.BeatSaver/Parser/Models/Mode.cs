using Newtonsoft.Json;
using System.Collections.Generic;

namespace SaberStudio.Services.BeatSaver.Parser.Models
{
    public class Mode
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("difficulties")]
        public Dictionary<string, MapData> Difficulties { get; set; }

    }
}
