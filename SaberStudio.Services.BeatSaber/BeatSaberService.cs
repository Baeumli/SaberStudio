using Newtonsoft.Json;
using SaberStudio.Core.Util;
using SaberStudio.Services.BeatSaber.Parser.Models;
using SaberStudio.Services.Settings.Interfaces;
using SaberStudio.Services.Steam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SaberStudio.Services.BeatSaber
{
    public class BeatSaberService : IBeatSaberService
    {
        private readonly ISteamService steamService;
        private readonly ISettingsStore settingsStore;

        public BeatSaberService(ISteamService steamService, ISettingsStore settingsStore)
        {
            this.steamService = steamService;
            this.settingsStore = settingsStore;
        }

        public IEnumerable<BeatMap> GetInstalledBeatMaps()
        {
            var levelsFolder = GetCustomLevelsDirectory();
            var mapPaths = Directory.EnumerateFiles(levelsFolder, "info.dat", SearchOption.AllDirectories);

            foreach (var mapPath in mapPaths)
            {
                var json = File.ReadAllText(mapPath);
                var map = JsonConvert.DeserializeObject<BeatMap>(json);
                map.FolderLocation = Path.GetDirectoryName(mapPath) ?? "";
                map.CoverImageFileName = Path.Combine(map.FolderLocation, map.CoverImageFileName);
                map.ImageUri = new Uri(map.CoverImageFileName);
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

            var playlists = Directory.EnumerateFiles(folder, "*.*").Where(s => s.EndsWith(".json", StringComparison.OrdinalIgnoreCase) 
                                                                               || s.EndsWith(".bplist", StringComparison.OrdinalIgnoreCase));

            foreach (var playlist in playlists)
            {
                var json = File.ReadAllText(playlist);
                yield return JsonConvert.DeserializeObject<Playlist>(json);
            }
        }

        public string GetPlaylistFolder()
        {
            var playlistFolder = Path.Combine(GetBaseDirectory(), "Playlists");

            Directory.CreateDirectory(playlistFolder);

            return playlistFolder;
        }

        public string GetGameVersion()
        {
            var gameManager = Path.Combine(GetBaseDirectory(), "Beat Saber_Data", "globalgamemanagers");
            using (var stream = File.OpenRead(gameManager))
            using (var reader = new BinaryReader(stream, Encoding.UTF8))
            {
                const string key = "public.app-category.games";
                var position = 0;

                while (stream.Position < stream.Length && position < key.Length)
                {
                    if (reader.ReadByte() == key[position])
                    {
                        position++;
                    }
                    else
                    {
                        position = 0;
                    }
                }

                if (stream.Position == stream.Length)
                    return null;

                while (stream.Position < stream.Length)
                {
                    var current = (char)reader.ReadByte();
                    if (char.IsDigit(current))
                        break;
                }

                var rewind = -sizeof(int) - sizeof(byte);
                stream.Seek(rewind, SeekOrigin.Current); // rewind to the string length

                var strlen = reader.ReadInt32();
                var strbytes = reader.ReadBytes(strlen);

                return Encoding.UTF8.GetString(strbytes);
            }
        }


        public string GetBaseDirectory()
        {
            var value = settingsStore.GetValueByKey("beatsaber.installdir") as string;

            if (!string.IsNullOrEmpty(value)) 
                return value;
            
            var steamFolder = steamService.GetBaseDirectory();
            var steamAppsFolder = Path.Combine(steamFolder, "steamapps");
            var beatSaberManifest = Path.Combine(steamAppsFolder, "appmanifest_620980.acf");
            var beatSaberFolder = Path.Combine(steamAppsFolder, "common", "Beat Saber");

            if (File.Exists(beatSaberManifest))
            {
                settingsStore.UpdateValue("beatsaber.installdir", beatSaberFolder);
                return beatSaberFolder;
            }
            
            var steamLibraryLocations = steamService.GetLibraryLocations(steamAppsFolder);

            foreach (var (key, location) in steamLibraryLocations)
            {
                beatSaberManifest = Path.Combine(location, "steamapps", "appmanifest_620980.acf");
                if (File.Exists(beatSaberManifest))
                    beatSaberFolder = Path.Combine(location, "steamapps", "common", "Beat Saber");
            }

            if (string.IsNullOrEmpty(beatSaberFolder))
                throw new DirectoryNotFoundException("Could not find Beat Saber. Please make sure Beat Saber is installed.");

            settingsStore.UpdateValue("beatsaber.installdir", beatSaberFolder);

            return beatSaberFolder;
        }

        public string GetCustomLevelsDirectory()
        {
            var customLevelsFolder = Path.Combine(GetBaseDirectory(), @"Beat Saber_Data\CustomLevels");

            if (!Directory.Exists(customLevelsFolder))
                throw new DirectoryNotFoundException();

            return customLevelsFolder;
        }

        public string GetPluginsDirectory()
        {
            var gameDirectory = GetBaseDirectory();
            return Path.Combine(gameDirectory, @"Beat Saber_Data\CustomLevels");
        }
    }
}
