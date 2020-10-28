using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SaberStudio.Services.BeatMods.Models.Parser
{
    public class Dependency
    {
        [JsonProperty("authorId")]
        public string AuthorId { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("downloads")]
        public IEnumerable<FileInfo> FileInfos { get; set; }

        [JsonProperty("gameVersion")]
        public string GameVersion { get; set; }

        [JsonProperty("link")]
        public string ModPageUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("required")]
        public bool IsRequired { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("uploadDate")]
        public DateTime UploadedAt { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("dependencies")]
        public IEnumerable<string> Dependencies { get; set; }
    }
}
