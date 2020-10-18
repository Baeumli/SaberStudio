using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SaberStudio.Services.BeatSaver.Interfaces
{
    public interface IBeatSaverClient
    {
        public Task<IEnumerable<Parser.Models.BeatMap>> GetByHash(CancellationToken cancellationToken, string hash);
        
        public Task<IEnumerable<Parser.Models.BeatMap>> GetByMapKey(CancellationToken cancellationToken, string mapKey);
               
        public Task<IEnumerable<Parser.Models.BeatMap>> GetByMapName(CancellationToken cancellationToken, string mapName);
        
        public Task<IEnumerable<Parser.Models.BeatMap>> GetByUploaderId(CancellationToken cancellationToken, string uploaderId, int pageNumber = 0);
        
        public Task<string> DownloadMap(CancellationToken cancellationToken, string downloadUrl, string fileName);
        
        public Task<IEnumerable<Parser.Models.BeatMap>> GetTrendingMaps(CancellationToken cancellationToken, int pageNumber = 0);
        
        public Task<IEnumerable<Parser.Models.BeatMap>> GetLatestMaps(CancellationToken cancellationToken, int pageNumber = 0);
        
        public Task<IEnumerable<Parser.Models.BeatMap>> GetMostPlayedMaps(CancellationToken cancellationToken, int pageNumber = 0);
        
        public Task<IEnumerable<Parser.Models.BeatMap>> GetMostDownloadedMaps(CancellationToken cancellationToken, int pageNumber = 0);
        
        public Task<IEnumerable<Parser.Models.BeatMap>> GetTopRatedMaps(CancellationToken cancellationToken, int pageNumber = 0);
    }
}
