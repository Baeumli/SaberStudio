using SaberStudio.Core.Extensions;
using SaberStudio.Services.BeatMods.Interfaces;
using SaberStudio.Services.BeatMods.Models;
using SaberStudio.Services.BeatMods.Models.Parser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SaberStudio.Services.BeatMods
{
    public class BeatModsClient : IBeatModsClient
    {
        private readonly HttpClient client;

        public BeatModsClient(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = new Uri("https://beatmods.com/api/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("SaberStudio/0.1");
        }

        public async Task<IEnumerable<Mod>> GetMods(CancellationToken cancellationToken, string gameVersion = "", string search = "", string sort = "", SortOrder sortOrder = SortOrder.Descending, string status = "")
        {
            var query = HttpUtility.ParseQueryString("");
            query["gameVersion"] = gameVersion;
            query["search"] = search;
            query["sort"] = sort;
            query["sortDirection"] = Convert.ToString((int)sortOrder);
            query["status"] = status;

            var request = new HttpRequestMessage(HttpMethod.Get, "mod?" + query.ToString());
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var mods = stream.DeserializeJsonFromStream<IEnumerable<Mod>>();
            return mods;
        }
    }
}
