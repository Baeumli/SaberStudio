using SaberStudio.Core.Extensions;
using SaberStudio.Services.BeatMods.Interfaces;
using SaberStudio.Services.BeatMods.Models;
using SaberStudio.Services.BeatMods.Models.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SaberStudio.Core.Util;
using SaberStudio.Services.BeatSaber;
using SaberStudio.Services.Settings.Interfaces;

namespace SaberStudio.Services.BeatMods
{
    public class BeatModsClient : IBeatModsClient
    {
        private readonly HttpClient client;
        private readonly IBeatSaberService beatSaberService;
        private readonly ISettingsStore settingsStore;

        private const string BeatModsAliasUrl = "https://alias.beatmods.com/aliases.json";
        private const string BeatModsVersionsUrl = "https://versions.beatmods.com/versions.json";
        private const string BeatModsApiUrl = "https://beatmods.com/api/v1/";
        private const string UserAgent = "SaberStudio/0.1";
        private const string JsonMediaType = "application/json";
        
        public BeatModsClient(HttpClient client, IBeatSaberService beatSaberService, ISettingsStore settingsStore)
        {
            this.client = client;
            this.beatSaberService = beatSaberService;
            this.settingsStore = settingsStore;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd(UserAgent);
        }

        public async Task<IEnumerable<Mod>> GetMods(CancellationToken cancellationToken, string gameVersion, string search = "", string sort = "", SortOrder sortOrder = SortOrder.Descending, string status = "")
        {
            var query = HttpUtility.ParseQueryString("");
            query["gameVersion"] = gameVersion;
            query["search"] = search;
            query["sort"] = sort;
            query["sortDirection"] = Convert.ToString((int)sortOrder);
            query["status"] = status;

            var request = new HttpRequestMessage(HttpMethod.Get, BeatModsApiUrl + "mod?" + query);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var mods = stream.DeserializeJsonFromStream<IEnumerable<Mod>>();
            return mods;
        }


        public async Task CheckInstalledMods(string gameVersion)
        {
            foreach (var mod in await GetMods(CancellationToken.None, gameVersion, status: Constants.Status.Approved))
            foreach (var modFile in mod.FileInfos)
            foreach (var checksum in modFile.Checksums)
            foreach (var checksums in GetChecksumsOfInstalledMods())
            {
                if (checksum.Md5Hash == checksums)
                {
                    Debug.WriteLine(mod.Name);
                }
            }
        }

        public IEnumerable<string> GetFileHashesFromMod(Mod mod)
        {
            return mod.FileInfos.SelectMany(x => x.Checksums).Select(x => x.Md5Hash);
        }

        public async Task<Mod> GetMod(CancellationToken cancellationToken, string gameVersion, string search, string status)
        {
            var query = HttpUtility.ParseQueryString("");
            query["gameVersion"] = gameVersion;
            query["search"] = search;
            query["status"] = status;

            var request = new HttpRequestMessage(HttpMethod.Get, BeatModsApiUrl + "mod?" + query);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var mod = stream.DeserializeJsonFromStream<IEnumerable<Mod>>().FirstOrDefault(x => x.Name == search);
            return mod;
        }

        public IEnumerable<string> GetChecksumsOfInstalledMods()
        {
            var pluginsDirectory = beatSaberService.GetPluginsDirectory();

            var installedMods = Directory.EnumerateFiles(pluginsDirectory)
                .Where(IsCorrectModFileExtension)
                .ToList();

            foreach (var installedMod in installedMods)
            {
                yield return FileHelper.CalculateMD5(installedMod);
            }
        }

        public async Task DownloadMod(CancellationToken cancellationToken, Mod mod)
        {
            var response = await client.GetAsync(BeatModsApiUrl + mod.FileInfos.First().DownloadUrl, cancellationToken);
            response.EnsureSuccessStatusCode();

            var tempPath = Path.Combine(Path.GetTempPath(), "SaberStudio");
            var zipFile = Path.Combine(tempPath, $"{mod.Name}_{mod.Version}.zip");

            await using var stream = await response.Content.ReadAsStreamAsync();
            await FileHelper.CreateFileFromStream(stream, zipFile, true);

            var gameDirectory = settingsStore.GetValueByKey("beatsaber.installdir") as string;
            var modFileHashes = GetFileHashesFromMod(mod);

            FileHelper.ExtractZip(zipFile, gameDirectory, modFileHashes);
        }

        public bool IsModInstalled(Mod mod)
        {
            var isInstalled = true;

            foreach (var checksum in mod.FileInfos.SelectMany(x => x.Checksums))
            {
                var localModFile = Path.Combine(beatSaberService.GetBaseDirectory(), checksum.File);

                if (!File.Exists(localModFile))
                {
                    isInstalled = false;
                    break;
                }

                var hash = FileHelper.CalculateMD5(localModFile);
                if (hash != checksum.Md5Hash)
                {
                    isInstalled = false;
                }
            }
            return isInstalled;
        }
        
        public async Task InstallModLoader(string version)
        {
            var modLoader = await GetMod(CancellationToken.None, version, "BSIPA", Constants.Status.Approved);

            if (IsModInstalled(modLoader))
                return;

            await DownloadMod(CancellationToken.None, modLoader);

            var baseDirectory = beatSaberService.GetBaseDirectory();
            
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = baseDirectory,
                FileName = Path.Combine(baseDirectory, "IPA.exe"),
                CreateNoWindow = true,
                Arguments = "-n"
            };

            using (var process = Process.Start(startInfo))
            {
                process?.WaitForExit();
            }
        }

        public async Task<Dictionary<string, IEnumerable<string>>> GetGameVersionAliases(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BeatModsAliasUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            
            var stream = await response.Content.ReadAsStreamAsync();
            var aliases = stream.DeserializeJsonFromStream<Dictionary<string, IEnumerable<string>>>();
            return aliases;
        }

        public async Task<IEnumerable<string>> GetGameVersions(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BeatModsVersionsUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            
            var stream = await response.Content.ReadAsStreamAsync();
            var versions = stream.DeserializeJsonFromStream<IEnumerable<string>>();
            return versions;
        }

        public async Task<string> GetGameVersionFromAlias(string versionAlias, CancellationToken cancellationToken)
        {
            var versions = await GetGameVersions(cancellationToken);
            var versionsWithAliases = await GetGameVersionAliases(cancellationToken);
            
            foreach (var version in versions)
            {
                if (!versionsWithAliases.TryGetValue(version, out var aliases))
                    continue;

                if (!aliases.Contains(versionAlias))
                    continue;

                return version;
            }
            return null;
        }

        public void UninstallModLoader()
        {

        }
        
        private static bool IsCorrectModFileExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            return extension == Core.Models.Constants.FileExtensions.Dll ||
                   extension == Core.Models.Constants.FileExtensions.Manifest;
        }
    }
}
