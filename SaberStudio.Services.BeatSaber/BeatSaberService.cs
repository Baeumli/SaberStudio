using Newtonsoft.Json;
using SaberStudio.Services.BeatSaber.Parser.Models;
using SaberStudio.Services.Steam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Prism.Events;
using SaberStudio.Core.Events;
using SaberStudio.Core.Util;
using SaberStudio.Services.BeatSaber.Utils;

namespace SaberStudio.Services.BeatSaber
{
    public class BeatSaberService : IBeatSaberService
    {
        private readonly ISteamService steamService;

        public BeatSaberService(ISteamService steamService)
        {
            this.steamService = steamService;
        }

        public IEnumerable<BeatMap> GetInstalledBeatMaps()
        {
            var levelsFolder = GetCustomLevelsDirectory();
            var mapPaths = Directory.EnumerateFiles(levelsFolder, "info.dat", SearchOption.AllDirectories);

            foreach (var mapPath in mapPaths)
            {
                var json = File.ReadAllText(mapPath);
                var map = JsonConvert.DeserializeObject<BeatMap>(json);
                map.FolderLocation = Path.GetDirectoryName(mapPath);
                map.CoverImageFileName = Path.Combine(map.FolderLocation, map.CoverImageFileName);
                yield return map;
            }
        }

        public void DeleteBeatMap(BeatMap beatMap)
        {
            var mapFolder = Path.Combine(GetCustomLevelsDirectory(), beatMap.FolderLocation);
            FileHelper.DeleteFolder(mapFolder, true);
        }

        public IEnumerable<Playlist> GetPlaylists()
        {
            var folder = GetPlaylistFolder();

            var playlists = Directory.EnumerateFiles(Path.GetDirectoryName(folder), "*.*")
                .Where(s => s.EndsWith(".json", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".bplist", StringComparison.OrdinalIgnoreCase));

            foreach (var playlist in playlists)
            {
                yield return JsonConvert.DeserializeObject<Playlist>(playlist);
            }
        }

        public string GetPlaylistFolder()
        {
            return Path.Combine(GetBaseDirectory(), "Playlists");
        }

        public string GetBaseDirectory()
        {
            var baseLocation = steamService.GetBaseDirectory();
            var steamAppsDirectory = Path.Combine(baseLocation, "steamapps");

            if (File.Exists(Path.Combine(steamAppsDirectory, "appmanifest_620980.acf")))
                return Path.Combine(steamAppsDirectory, "common", "Beat Saber");

            var locations = steamService.GetLibraryLocations(steamAppsDirectory);

            foreach (var location in locations)
            {
                if (File.Exists(Path.Combine(location.Value, "steamapps", "appmanifest_620980.acf")))
                    return Path.Combine(location.Value, "steamapps", "common", "Beat Saber");
            }
            throw new DirectoryNotFoundException("Could not find Beat Saber directory");
        }

        public string GetCustomLevelsDirectory()
        {
            return Path.Combine(GetBaseDirectory(), @"Beat Saber_Data\CustomLevels");
        }
        
        
    }
}
