using System.Threading;
using System.Threading.Tasks;
using SaberStudio.Services.BeatSaver.Models;

namespace SaberStudio.Services.BeatSaver.Interfaces
{
    public interface IBeatSaverClient
    {
        public Task<MapDetail> GetMap(string id, CancellationToken cancellationToken);
        public Task<MapDetail> GetMapsByHash(string hash, CancellationToken cancellationToken);
        public Task<SearchResponse> GetMapsByUser(string userId, CancellationToken cancellationToken, int? pageNumber = 0);
        public Task<SearchResponse> GetLatestMaps(string before, string after, bool automapper, string sort, CancellationToken cancellationToken);
        public Task<SearchResponse> GetMostPlayedMaps(CancellationToken cancellationToken, int? pageNumber = 0);
    }
}
