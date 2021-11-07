using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class MapVersion
    {
        [JsonProperty("coverURL")]
        public string CoverUrl { get; set; }     
        
        [JsonProperty("createdAt")]
        public DateTime CreateDate { get; set; }   
        
        [JsonProperty("diffs")]
        public IEnumerable<MapDifficulty> Difficulties { get; set; }   
        
        [JsonProperty("downloadURL")]
        public string DownloadUrl { get; set; }  
        
        [JsonProperty("feedback")]
        public string Feedback { get; set; }  
        
        [JsonProperty("hash")]
        public string Hash { get; set; }    
        
        [JsonProperty("key")]
        public string Key { get; set; }    
        
        [JsonProperty("previewURL")]
        public string PreviewUrl { get; set; }    
        
        [JsonProperty("sageScore")]
        public short SageScore { get; set; }   
        
        [JsonProperty("state")]
        public State State { get; set; }    
        
        [JsonProperty("testplayAt")]
        public DateTime TestPlayDate { get; set; }    
        
        [JsonProperty("testplays")]
        public IEnumerable<MapTestPlay> TestPlays { get; set; }    
        
    }
}