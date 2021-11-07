using System.Collections.Generic;
using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class SearchResponse
    {
        [JsonProperty("docs")]
        public IEnumerable<MapDetail> Maps { get; set; }

        [JsonProperty("redirect")]
        public string Redirect { get; set; }
    }
}