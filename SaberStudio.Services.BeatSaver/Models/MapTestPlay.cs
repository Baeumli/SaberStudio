using System;
using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class MapTestPlay
    {
        [JsonProperty("createdAt")]
        public DateTime CreateDate { get; set; }
        
        [JsonProperty("feedback")]
        public string Feedback { get; set; }
        
        [JsonProperty("feedbackAt")]
        public DateTime FeedbackDate { get; set; }
        
        [JsonProperty("user")]
        public UserDetail User { get; set; }
        
        [JsonProperty("video")]
        public string Video { get; set; }
    }
}