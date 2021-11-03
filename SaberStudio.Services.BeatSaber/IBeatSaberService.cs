using SaberStudio.Services.BeatSaber.Parser.Models;
using System.Collections.Generic;

namespace SaberStudio.Services.BeatSaber
{
    public interface IBeatSaberService
    {
        public string GetBaseDirectory();
        public string GetCustomLevelsDirectory();
        public string GetPluginsDirectory();
        public IEnumerable<BeatMap> GetInstalledBeatMaps();
        public void DeleteBeatMap(BeatMap beatMap);
        public IEnumerable<Playlist> GetPlaylists();
        public string GetGameVersion();

    }
}