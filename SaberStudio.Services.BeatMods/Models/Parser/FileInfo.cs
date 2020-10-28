using Newtonsoft.Json;
using System.Collections.Generic;

namespace SaberStudio.Services.BeatMods.Models.Parser
{
    public class FileInfo
    {
        [JsonProperty("hashMd5")]
        public IEnumerable<Checksum> Checksums { get; set; }
        [JsonProperty("type")]
        public string DownloadType { get; set; }
        [JsonProperty("url")]
        public string DownloadUrl { get; set; }
    }
}
