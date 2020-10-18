using Newtonsoft.Json;
using System;

namespace SaberStudio.Services.BeatSaver.Parser.Models
{
    public class BeatMap
    {
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("stats")]
        public Statistics Statistics { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("_id")]
        public string MapId { get; set; }

        [JsonProperty("key")]
        public string MapKey { get; set; }

        [JsonProperty("name")]
        public string MapName { get; set; }

        [JsonProperty("uploader")]
        public Uploader Uploader { get; set; }

        [JsonProperty("uploaded")]
        public DateTime UploadDate { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("directDownload")]
        public string DownloadUrl { get; set; }

        [JsonProperty("coverURL")]
        public string CoverUrl { get; set; }

    }
}
