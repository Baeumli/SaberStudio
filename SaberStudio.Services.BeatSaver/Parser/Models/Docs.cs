using Newtonsoft.Json;
using System.Collections.Generic;

namespace SaberStudio.Services.BeatSaver.Parser.Models
{
    public class Docs
    {
        [JsonProperty("docs")]
        public IEnumerable<BeatMap> BeatMaps { get; set; }
    }
}
