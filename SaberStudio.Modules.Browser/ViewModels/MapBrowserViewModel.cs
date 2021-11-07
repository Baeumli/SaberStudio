using System.Collections.Generic;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Core.Extensions;
using SaberStudio.Modules.Browser.Views;
using SaberStudio.Services.BeatSaver.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using SaberStudio.Services.BeatSaber;
using SaberStudio.Services.BeatSaber.Parser.Models;
using SaberStudio.Services.BeatSaver.Models;
using SaberStudio.UI;
using SaberStudio.UI.Models;

namespace SaberStudio.Modules.Browser.ViewModels
{
    public class MapBrowserViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IBeatSaverClient beatSaverClient;
        private readonly IBeatSaberService beatSaberService;

        #region Properties

        public ObservableCollection<MapDetail> BeatMaps { get; set; } = new ObservableCollection<MapDetail>();
        public List<MenuItem> Categories { get; set; } = new List<MenuItem>();
        public ObservableCollection<BeatMap> WeeklyTrends { get; set; } = new ObservableCollection<BeatMap>();
        public ObservableCollection<Playlist> Playlists { get; set; } = new ObservableCollection<Playlist>();

        private MenuItem selectedCategory;
        public MenuItem SelectedCategory
        {
            get => selectedCategory;
            set => SetProperty(ref selectedCategory, value);
        }

        private BeatMap selectedBeatMap;
        public BeatMap SelectedBeatMap
        {
            get => selectedBeatMap;
            set => SetProperty(ref selectedBeatMap, value);
        }

        private int currentPage = 0;

        public int CurrentPage
        {
            get => currentPage;
            set => SetProperty(ref currentPage, value);
        }

        #endregion

        public MapBrowserViewModel(IBeatSaverClient beatSaverClient, IRegionManager regionManager, IBeatSaberService beatSaberService)
        {
            this.regionManager = regionManager;
            this.beatSaverClient = beatSaverClient;
            this.beatSaberService = beatSaberService;
        }

        #region Commands

        private DelegateCommand selectedCommand;
        public DelegateCommand SelectedCommand => selectedCommand ??= new DelegateCommand(ExecuteSelectedCommand);

        private void ExecuteSelectedCommand()
        {
            var navParams = new NavigationParameters
            {
                { "BeatMap",  SelectedBeatMap }
            };

            regionManager.RequestNavigate(Regions.ContentRegion, nameof(MapDetailView), navParams);
        }

        private DelegateCommand selectCategoryCommand;
        public DelegateCommand SelectCategoryCommand => selectCategoryCommand ??= new DelegateCommand(ExecuteSelectCategoryCommand);

        private void ExecuteSelectCategoryCommand()
        {
            GetMapsByCategory(SelectedCategory.Parameter.ToString());
        }

        private DelegateCommand downloadCommand;
        public DelegateCommand DownloadCommand => downloadCommand ??= new DelegateCommand(ExecuteDownloadCommand);

        private void ExecuteDownloadCommand()
        {
            //beatSaverClient.DownloadMap(CancellationToken.None, SelectedBeatMap);
        }

        private DelegateCommand goToPreviousPageCommand;
        public DelegateCommand GoToPreviousPageCommand => goToPreviousPageCommand ??= new DelegateCommand(ExecuteGoToPreviousPageCommand, CanGoBack);

        private void ExecuteGoToPreviousPageCommand()
        {
            CurrentPage--;
            GoToPreviousPageCommand.RaiseCanExecuteChanged();
            GoToNextPageCommand.RaiseCanExecuteChanged();
        }

        private bool CanGoBack()
        {
            return CurrentPage > 0;
        }

        private DelegateCommand goToNextPageCommand;
        public DelegateCommand GoToNextPageCommand => goToNextPageCommand ??= new DelegateCommand(ExecuteGoToNextPageCommand);

        private void ExecuteGoToNextPageCommand()
        {
            CurrentPage++;
            GoToPreviousPageCommand.RaiseCanExecuteChanged();
            GoToNextPageCommand.RaiseCanExecuteChanged();
        }

        #endregion

        private async void GetMapsByCategory(string category, int? page = null)
        {
            //var pageNumber = page ?? CurrentPage;

            var response = await beatSaverClient.GetLatestMaps(null, null, false, null, CancellationToken.None);
            
            BeatMaps.Clear();
            BeatMaps.AddRange(response.Maps);
        }

        private void PopulateCategories()
        {
            if (Categories.Any())
                return;
            
            var categories = new List<MenuItem>()
            {
                new MenuItem { Name = "Hot", Parameter = "Hot" },
                new MenuItem { Name = "Rating", Parameter = "Rating" },
                new MenuItem { Name = "Latest", Parameter = "Latest" },
                new MenuItem { Name = "Downloads", Parameter = "Downloads" },
                new MenuItem { Name = "Plays", Parameter = "Plays" },
            };
            Categories.AddRange(categories);
        }

        #region Navigation

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            PopulateCategories();
            SelectedCategory = Categories.First();
            ExecuteSelectCategoryCommand();
            Playlists = beatSaberService.GetPlaylists().ToObservableCollection();
        }

        #endregion

    }
}
