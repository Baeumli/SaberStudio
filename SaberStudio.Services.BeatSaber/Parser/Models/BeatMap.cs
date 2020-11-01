using Newtonsoft.Json;
using System.Collections.Generic;

namespace SaberStudio.Services.BeatSaber.Parser.Models
{
    public class BeatMap
    {
        public string FolderLocation { get; set; }

        [JsonProperty("_version")]
        public string Version { get; set; }

        [JsonProperty("_songName")]
        public string SongName { get; set; }

        [JsonProperty("_songSubName")]
        public string SongSubName { get; set; }

        [JsonProperty("_songAuthorName")]
        public string SongAuthorName { get; set; }

        [JsonProperty("_levelAuthorName")]
        public string LevelAuthorName { get; set; }

        [JsonProperty("_beatsPerMinute")]
        public decimal BeatsPerMinute { get; set; }

        [JsonProperty("_songTimeOffset")]
        public decimal SongTimeOffset { get; set; }

        [JsonProperty("_shuffle")]
        public decimal Shuffle { get; set; }

        [JsonProperty("_shufflePeriod")]
        public decimal ShufflePeriod { get; set; }

        [JsonProperty("_previewStartTime")]
        public decimal PreviewStartTime { get; set; }

        [JsonProperty("_previewDuration")]
        public decimal PreviewDuration { get; set; }

        [JsonProperty("_songFilename")]
        public string SongFileName { get; set; }

        [JsonProperty("_coverImageFilename")]
        public string CoverImageFileName { get; set; }

        [JsonProperty("_environmentName")]
        public string EnvironmentName { get; set; }

        [JsonProperty("_customData")]
        public CustomData CustomData { get; set; }

        [JsonProperty("_difficultyBeatmapSets")]
        public IEnumerable<DifficultyBeatMapSet> DifficultyBeatMapSets { get; set; }

    }

    public class CustomData
    {
        [JsonProperty("_contributors")]
        public IEnumerable<string> Contributors { get; set; }

        [JsonProperty("_customEnvironment")]
        public string CustomEnvironment { get; set; }

        [JsonProperty("_customEnvironmentHash")]
        public string CustomEnvironmentHash { get; set; }
    }

}
