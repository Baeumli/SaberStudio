using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Core;
using SaberStudio.Core.Extensions;
using SaberStudio.Modules.Library.Views;
using SaberStudio.Services.BeatSaber;
using SaberStudio.Services.BeatSaber.Parser.Models;
using System.Collections.ObjectModel;

namespace SaberStudio.Modules.Library.ViewModels
{
    public class MapLibraryViewModel : BindableBase, INavigationAware
    {
        private readonly IBeatSaberService beatSaberService;
        private readonly IRegionManager regionManager;

        private ObservableCollection<BeatMap> beatMaps;
        public ObservableCollection<BeatMap> BeatMaps
        {
            get { return beatMaps; }
            set { SetProperty(ref beatMaps, value); }
        }

        public MapLibraryViewModel(IBeatSaberService beatSaberService, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.beatSaberService = beatSaberService;
        }

        private DelegateCommand<BeatMap> selectedCommand;
        public DelegateCommand<BeatMap> SelectedCommand => selectedCommand ??= new DelegateCommand<BeatMap>(ExecuteSelectedCommand);

        private void ExecuteSelectedCommand(BeatMap map)
        {
            var navParams = new NavigationParameters
                {
                    { "BeatMap",  map }
                };

            regionManager.RequestNavigate(Regions.ContentRegion, typeof(MapLibraryDetailView).Name, navParams);
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
