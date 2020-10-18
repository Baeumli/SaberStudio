using Newtonsoft.Json;
using System.Collections.Generic;

namespace SaberStudio.Services.BeatSaber.Parser.Models
{
    public class DifficultyBeatMapSet
    {
        [JsonProperty("_beatmapCharacteristicName")]
        public string BeatMapCharacteristicName { get; set; }

        [JsonProperty("_difficultyBeatmaps")]
        public IEnumerable<DifficultyBeatMap> DifficultyBeatMaps { get; set; }

        public class DifficultyBeatMap
        {
            [JsonProperty("_difficulty")]
            public string Difficulty { get; set; }

            [JsonProperty("_difficultyRank")]
            public int DifficultyRank { get; set; }

            [JsonProperty("_beatmapFilename")]
            public string BeatMapFileName { get; set; }

            [JsonProperty("_noteJumpMovementSpeed")]
            public decimal NoteJumpMovementSpeed { get; set; }

            [JsonProperty("_noteJumpStartBeatOffset")]
            public decimal NoteJumpStartBeatOffset { get; set; }

            [JsonProperty("_customData")]
            public CustomData CustomData { get; set; }

        }

        public class CustomData
        {
            [JsonProperty("_difficultyLabel")]
            public string DifficultyLabel { get; set; }

            [JsonProperty("_editorOffset")]
            public int EditorOffset { get; set; }

            [JsonProperty("_editorOldOffset")]
            public int EditorOldOffset { get; set; }

            [JsonProperty("_warnings")]
            public IEnumerable<string> Warnings { get; set; }

            [JsonProperty("_information")]
            public IEnumerable<string> Information { get; set; }

            [JsonProperty("_suggestions")]
            public IEnumerable<string> Suggestions { get; set; }

            [JsonProperty("_requirements")]
            public IEnumerable<string> Requirements { get; set; }

        }

    }
}
