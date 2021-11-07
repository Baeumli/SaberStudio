using System;
using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class UserStats
    {
        [JsonProperty("avgBpm")]
        public float AverageBpm { get; set; }
        
        [JsonProperty("avgDuration")]
        public float AverageDuration { get; set; }
        
        [JsonProperty("avgScore")]
        public float AverageScore { get; set; }
        
        [JsonProperty("diffStats")]
        public UserDiffStats DiffStats { get; set; }
        
        [JsonProperty("firstUpload")]
        public DateTime FirstUploadDate { get; set; }
        
        [JsonProperty("lastUpload")]
        public DateTime LastUploadDate { get; set; }
        
        [JsonProperty("rankedMaps")]
        public int RankedMaps { get; set; }
        
        [JsonProperty("totalDownvotes")]
        public int TotalDownVotes { get; set; }
        
        [JsonProperty("totalMaps")]
        public int TotalMaps { get; set; }
        
        [JsonProperty("totalUpvotes")]
        public int TotalUpVotes { get; set; }
    }
}