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
    }
}