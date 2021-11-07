using Newtonsoft.Json;

namespace SaberStudio.Services.BeatSaver.Models
{
    public class MapDifficulty
    {
        [JsonProperty("bombs")] 
        public int Bombs { get; set; }
        
        [JsonProperty("characteristic")] 
        public Characteristic Characteristic { get; set; }
        
        [JsonProperty("chroma")] 
        public bool IsChroma { get; set; }
        
        [JsonProperty("cinema")] 
        public bool IsCinema { get; set; }
        
        [JsonProperty("difficulty")] 
        public Difficulty Difficulty { get; set; }
        
        [JsonProperty("events")] 
        public int Events { get; set; }
        
        [JsonProperty("length")] 
        public double Length { get; set; }
        
        [JsonProperty("me")] 
        public bool Me { get; set; }
        
        [JsonProperty("ne")] 
        public bool Ne { get; set; }
        
        [JsonProperty("njs")] 
        public float Njs { get; set; }
        
        [JsonProperty("notes")] 
        public int Notes { get; set; }
        
        [JsonProperty("nps")] 
        public double NotesPerSecond { get; set; }
        
        [JsonProperty("obstacles")] 
        public int Obstacles { get; set; }
        
        [JsonProperty("offset")] 
        public float Offset { get; set; }
        
        [JsonProperty("paritySummary")] 
        public MapParitySummary ParitySummary { get; set; }
        
        [JsonProperty("seconds")] 
        public double Seconds { get; set; }
        
        [JsonProperty("stars")] 
        public float Stars { get; set; }
    }
}