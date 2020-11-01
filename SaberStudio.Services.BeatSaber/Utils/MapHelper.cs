using SaberStudio.Services.BeatSaber.Parser.Models;

namespace SaberStudio.Services.BeatSaber.Utils
{
    public static class MapHelper
    {
        public static string GetFormattedMapName(string mapKey, string songName, string levelAuthor)
        {
            return $"{mapKey} ({songName} - {levelAuthor})";
        }

    }
}