using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Modules.Library.Views;
using SaberStudio.Services.BeatSaber;
using SaberStudio.Services.BeatSaber.Parser.Models;
using System.Collections.ObjectModel;
using Prism.Events;
using SaberStudio.Core.Events;
using SaberStudio.UI;

namespace SaberStudio.Modules.Library.ViewModels
{
    public class MapLibraryViewModel : BindableBase, INavigationAware
    {
        private readonly IBeatSaberService beatSaberService;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;

        #region Properties

        public ObservableCollection<BeatMap> BeatMaps { get; set; }

        #endregion

        public MapLibraryViewModel(IBeatSaberService beatSaberService, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.beatSaberService = beatSaberService;
            BeatMaps = new ObservableCollection<BeatMap>();
            UpdateMapCollection();
        }

        #region Commands

        private DelegateCommand<BeatMap> selectedCommand;
        public DelegateCommand<BeatMap> SelectedCommand => selectedCommand ??= new DelegateCommand<BeatMap>(ExecuteSelectedCommand);

        private void ExecuteSelectedCommand(BeatMap map)
        {
            var navParams = new NavigationParameters
            {
                { "BeatMap",  map }
            };

            regionManager.RequestNavigate(Regions.ContentRegion, nameof(MapLibraryDetailView), navParams);
        }

        private DelegateCommand<BeatMap> deleteMapCommand;
        public DelegateCommand<BeatMap> DeleteMapCommand => deleteMapCommand ??= new DelegateCommand<BeatMap>(ExecuteDeleteMapCommand);

        private void ExecuteDeleteMapCommand(BeatMap map)
        {
            beatSaberService.DeleteBeatMap(map);
            eventAggregator.GetEvent<MapLibraryChangedEvent>().Publish();
        }

        #endregion

        private void UpdateMapCollection()
        {
            var maps = beatSaberService.GetInstalledBeatMaps();
            BeatMaps.Clear();
            BeatMaps.AddRange(maps);
        }

        #region Navigation

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            eventAggregator.GetEvent<MapLibraryChangedEvent>().Unsubscribe(UpdateMapCollection);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            eventAggregator.GetEvent<MapLibraryChangedEvent>().Subscribe(UpdateMapCollection);
        }

        #endregion

    }
}
