using SaberStudio.Services.BeatMods.Models;
using SaberStudio.Services.BeatMods.Models.Parser;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static SaberStudio.Services.BeatMods.Models.Constants;

namespace SaberStudio.Services.BeatMods.Interfaces
{
    public interface IBeatModsClient
    {

        public Task<IEnumerable<Mod>> GetMods(CancellationToken cancellationToken, string gameVersion, string search, string sort, SortOrder sortOrder, string status);
    }
}
