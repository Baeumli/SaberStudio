using SaberStudio.Services.BeatMods.Models;
using SaberStudio.Services.BeatMods.Models.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace SaberStudio.Services.BeatMods.Interfaces
{
    public interface IBeatModsClient
    {

        public Task<IEnumerable<Mod>> GetMods(CancellationToken cancellationToken, string gameVersion, string search, string sort, SortOrder sortOrder, string status);
        public Task DownloadMod(CancellationToken cancellationToken, Mod mod);
        public Task CheckInstalledMods(string gameVersion);
        public Task<Mod> GetMod(CancellationToken cancellationToken, string gameVersion, string search, string status);
        public bool IsModInstalled(Mod mod);
        public Task InstallModLoader(string version);
        public Task<ImmutableDictionary<string, IEnumerable<string>>> GetGameVersionAliases(CancellationToken cancellationToken);
        public Task<IEnumerable<string>> GetGameVersions(CancellationToken cancellationToken);
        public Task<string> GetGameVersionFromAlias(string versionAlias, CancellationToken cancellationToken);


    }
}
