using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SaberStudio.Core.Util;
using SaberStudio.Services.BeatMods;
using SaberStudio.Services.BeatSaber;

namespace SaberStudio.Services.Bsipa
{
    public class BsipaService : IBsipaService
    {
        private readonly BeatSaberService beatSaberService;
        private readonly BeatModsClient beatModsClient;
        
        public BsipaService(BeatSaberService beatSaberService, BeatModsClient beatModsClient)
        {
            this.beatSaberService = beatSaberService;
            this.beatModsClient = beatModsClient;
        }

        public async Task<bool> IsBsipaInstalled()
        {
            var beatSaberDirectory = beatSaberService.GetBaseDirectory();
            
            var injectorPath = Path.Combine(beatSaberDirectory, "Beat Saber_Data", "Managed", "IPA.Injector.dll");
            if (!File.Exists(injectorPath)) return false;
            
            var injectorHash = FileHelper.CalculateMD5(injectorPath);
            var mods = await beatModsClient.GetMods(CancellationToken.None);
            var bsipa = mods.First(x => x.Name.ToLower() == "bsipa");
            return bsipa.FileInfos.All(x => x.Checksums.All(y => y.Md5Hash == injectorHash));
        }
    }
}