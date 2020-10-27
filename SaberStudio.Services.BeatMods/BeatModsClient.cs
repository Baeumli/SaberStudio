using SaberStudio.Services.BeatMods.Interfaces;
using System;
using System.Net.Http;
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

//        var query = HttpUtility.ParseQueryString(string.Empty);
//        query["foo"] = "bar<>&-baz";
//query["bar"] = "bazinga";
//string queryString = query.ToString();


//        mod?search=&status=approved&gameVersion=1.12.2&sort=&sortDirection=1
    }
}
