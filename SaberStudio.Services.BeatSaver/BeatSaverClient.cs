using SaberStudio.Core.Extensions;
using SaberStudio.Services.BeatSaver.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Prism.Events;
using SaberStudio.Core.Events;
using SaberStudio.Core.Util;
using SaberStudio.Services.BeatSaber;
using SaberStudio.Services.BeatSaber.Utils;
using SaberStudio.Services.BeatSaver.Parser.Models;

namespace SaberStudio.Services.BeatSaver
{
    public class BeatSaverClient : IBeatSaverClient
    {
        private readonly HttpClient client;
        private readonly IBeatSaberService beatSaberService;
        private readonly IEventAggregator eventAggregator;
        public BeatSaverClient(HttpClient client, IBeatSaberService beatSaberService, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.beatSaberService = beatSaberService;
            this.client = client;
            client.BaseAddress = new Uri("https://beatsaver.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("SaberStudio/0.1");
        }

        public async Task DownloadMap(CancellationToken cancellationToken, BeatMap map)
        {
            var folderName = MapHelper.GetFormattedMapName(map.MapKey, map.Metadata.SongName, map.Metadata.SongAuthor);
            var levelsDirectory = beatSaberService.GetCustomLevelsDirectory();
            var savePath = Path.Combine(levelsDirectory, folderName);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/zip"));
            var response = await client.GetAsync(map.DownloadUrl, cancellationToken);

            var tempPath = Path.Combine(Path.GetTempPath(), "SaberStudio", folderName);
            var zipFile = Path.Combine(tempPath, folderName + ".zip");
            
            FileHelper.CreateFolder(tempPath);

            await using var stream = await response.Content.ReadAsStreamAsync();
            await FileHelper.CreateFileFromStream(stream, zipFile, true);

            FileHelper.ExtractZip(zipFile, savePath);
            eventAggregator.GetEvent<MapLibraryChangedEvent>().Publish();
        }

        public async Task<IEnumerable<Parser.Models.BeatMap>> GetByHash(CancellationToken cancellationToken, string hash)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "maps/by-hash/" + hash);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var docs = stream.DeserializeJsonFromStream<Docs>();

            return docs.BeatMaps;
        }

        public Task<IEnumerable<Parser.Models.BeatMap>> GetByMapKey(CancellationToken cancellationToken, string mapKey)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Parser.Models.BeatMap>> GetByMapName(CancellationToken cancellationToken, string searchQuery, int pageNumber = 0)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "search/text/" + pageNumber + "?q=" + searchQuery);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var docs = stream.DeserializeJsonFromStream<Parser.Models.Docs>();

            return docs.BeatMaps;
        }
        
        public Task<IEnumerable<Parser.Models.BeatMap>> GetByUploaderId(CancellationToken cancellationToken, string uploaderId, int pageNumber = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Parser.Models.BeatMap>> GetLatestMaps(CancellationToken cancellationToken, int pageNumber = 0)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "maps/latest/" + pageNumber);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var docs = stream.DeserializeJsonFromStream<Parser.Models.Docs>();

            return docs.BeatMaps;
        }

        public async Task<IEnumerable<Parser.Models.BeatMap>> GetMostDownloadedMaps(CancellationToken cancellationToken, int pageNumber = 0)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "maps/downloads/" + pageNumber);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var docs = stream.DeserializeJsonFromStream<Parser.Models.Docs>();

            return docs.BeatMaps;
        }

        public async Task<IEnumerable<Parser.Models.BeatMap>> GetMostPlayedMaps(CancellationToken cancellationToken, int pageNumber = 0)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "maps/plays/" + pageNumber);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var docs = stream.DeserializeJsonFromStream<Parser.Models.Docs>();

            return docs.BeatMaps;
        }

        public async Task<IEnumerable<Parser.Models.BeatMap>> GetTopRatedMaps(CancellationToken cancellationToken, int pageNumber = 0)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "maps/rating/" + pageNumber);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var docs = stream.DeserializeJsonFromStream<Parser.Models.Docs>();

            return docs.BeatMaps;
        }

        public async Task<IEnumerable<Parser.Models.BeatMap>> GetTrendingMaps(CancellationToken cancellationToken, int pageNumber = 0)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "maps/hot/" + pageNumber);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var docs = stream.DeserializeJsonFromStream<Parser.Models.Docs>();

            return docs.BeatMaps;
        }
    }
}