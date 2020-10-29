using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Core.Extensions;
using SaberStudio.Services.BeatSaber;
using SaberStudio.Services.BeatSaber.Parser.Models;
using System.Collections.ObjectModel;

namespace SaberStudio.Modules.Library.ViewModels
{
    public class MapLibraryViewModel : BindableBase, INavigationAware
    {
        private readonly IBeatSaberService beatSaberService;

        private ObservableCollection<BeatMap> beatMaps;
        public ObservableCollection<BeatMap> BeatMaps
        {
            get { return beatMaps; }
            set { SetProperty(ref beatMaps, value); }
        }

        public MapLibraryViewModel(IBeatSaberService beatSaberService)
        {
            this.beatSaberService = beatSaberService;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var maps = beatSaberService.GetInstalledBeatMaps();
            BeatMaps = maps.ToObservableCollection();
        }
    }
}
