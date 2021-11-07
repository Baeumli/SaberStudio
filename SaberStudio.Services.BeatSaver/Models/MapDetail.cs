using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class MapDetail
    {
        [JsonProperty("automapper")]
        public bool IsAutomapped { get; set; }
        
        [JsonProperty("createdAt")]
        public DateTime CreateDate { get; set; }
        
        [JsonProperty("curator")]
        public string Curator { get; set; }
        
        [JsonProperty("deletedAt")]
        public DateTime DeleteDate { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("lastPublishedAt")]
        public DateTime LastPublishDate { get; set; }
        
        [JsonProperty("metadata")]
        public MapDetailMetadata Metadata { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("qualified")]
        public bool IsQualified { get; set; }
        
        [JsonProperty("ranked")]
        public bool IsRanked { get; set; }
        
        [JsonProperty("stats")]
        public MapStats Stats { get; set; }
        
        [JsonProperty("updatedAt")]
        public DateTime UpdateDate { get; set; }
        
        [JsonProperty("uploaded")]
        public DateTime UploadDate { get; set; }
        
        [JsonProperty("uploader")]
        public UserDetail Uploader { get; set; }
        
        [JsonProperty("versions")]
        public IEnumerable<MapVersion> Versions { get; set; }
    }
}