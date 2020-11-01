using SaberStudio.Services.BeatSaber.Parser.Models;
using System.Collections.Generic;

namespace SaberStudio.Services.BeatSaber
{
    public interface IBeatSaberService
    {
        string GetBaseDirectory();
        string GetCustomLevelsDirectory();
        IEnumerable<BeatMap> GetInstalledBeatMaps();
        void DeleteBeatMap(BeatMap beatMap);
    }
}