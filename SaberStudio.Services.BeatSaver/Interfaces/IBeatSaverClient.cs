using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SaberStudio.Services.BeatSaver.Parser.Models;

namespace SaberStudio.Services.BeatSaver.Interfaces
{
    public interface IBeatSaverClient
    {
        public Task<IEnumerable<BeatMap>> GetByHash(CancellationToken cancellationToken, string hash);
        
        public Task<IEnumerable<BeatMap>> GetByMapKey(CancellationToken cancellationToken, string mapKey);
               
        public Task<IEnumerable<BeatMap>> GetByFuzzySearch(CancellationToken cancellationToken, string searchQuery, int pageNumber = 0);
        
        public Task<IEnumerable<BeatMap>> GetByUploaderId(CancellationToken cancellationToken, string uploaderId, int pageNumber = 0);
        
        public Task DownloadMap(CancellationToken cancellationToken, BeatMap map);
        
        public Task<IEnumerable<BeatMap>> GetTrendingMaps(CancellationToken cancellationToken, int pageNumber = 0);
        
        public Task<IEnumerable<BeatMap>> GetLatestMaps(CancellationToken cancellationToken, int pageNumber = 0);
        
        public Task<IEnumerable<BeatMap>> GetMostPlayedMaps(CancellationToken cancellationToken, int pageNumber = 0);
        
        public Task<IEnumerable<BeatMap>> GetMostDownloadedMaps(CancellationToken cancellationToken, int pageNumber = 0);
        
        public Task<IEnumerable<BeatMap>> GetTopRatedMaps(CancellationToken cancellationToken, int pageNumber = 0);
    }
}
