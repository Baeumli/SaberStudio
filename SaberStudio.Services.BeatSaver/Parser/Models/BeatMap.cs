using Newtonsoft.Json;
using System;

namespace SaberStudio.Services.BeatSaver.Parser.Models
{
    public class BeatMap
    {
        [JsonProperty("coverURL")]
        public string CoverUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("directDownload")]
        public string DirectDownload { get; set; }

        [JsonProperty("downloadURL")]
        public string DownloadUrl { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("key")]
        public string MapKey { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("name")]
        public string MapName { get; set; }

        [JsonProperty("stats")]
        public Statistics Statistics { get; set; }

        [JsonProperty("uploaded")]
        public DateTime UploadDate { get; set; }

        [JsonProperty("uploader")]
        public Uploader Uploader { get; set; }

        [JsonProperty("_id")]
        public string MapId { get; set; }
    }
}
