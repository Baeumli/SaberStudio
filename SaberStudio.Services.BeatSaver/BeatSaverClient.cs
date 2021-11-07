using SaberStudio.Core.Extensions;
using SaberStudio.Services.BeatSaver.Interfaces;
using SaberStudio.Services.BeatSaver.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SaberStudio.Services.BeatSaver
{
    public class BeatSaverClient : IBeatSaverClient
    {
        private readonly HttpClient client;
        
        
        public BeatSaverClient(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = new Uri("https://api.beatsaver.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("SaberStudio/0.1");
        }
        
        public async Task<MapDetail> GetMap(string id, CancellationToken cancellationToken)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"maps/id/{id}");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.DeserializeJsonFromStream<MapDetail>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public async Task<MapDetail> GetMapsByHash(string hash, CancellationToken cancellationToken)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"maps/hash/{hash}");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.DeserializeJsonFromStream<MapDetail>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public async Task<SearchResponse> GetMapsByUser(string userId, CancellationToken cancellationToken, int? pageNumber)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"maps/hash/{userId}/{pageNumber}");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.DeserializeJsonFromStream<SearchResponse>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public async Task<SearchResponse> GetLatestMaps(string before, string after, bool automapper, string sort, CancellationToken cancellationToken)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"maps/latest");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.DeserializeJsonFromStream<SearchResponse>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public async Task<SearchResponse> GetMostPlayedMaps(CancellationToken cancellationToken, int? pageNumber)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"maps/plays/{pageNumber}");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.DeserializeJsonFromStream<SearchResponse>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }
    }
}