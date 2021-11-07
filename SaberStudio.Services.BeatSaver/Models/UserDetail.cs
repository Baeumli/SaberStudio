using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class UserDetail
    {
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("hash")]
        public string Hash { get; set; }
        
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("stats")]
        public UserStats Stats { get; set; }
        
        [JsonProperty("testplay")]
        public bool TestPlay { get; set; }
        
        [JsonProperty("type")]
        public Type Type { get; set; }
        
        [JsonProperty("uniqueSet")]
        public bool UniqueSet { get; set; }
        
        [JsonProperty("uploadLimit")]
        public int UploadLimit { get; set; }
    }
}