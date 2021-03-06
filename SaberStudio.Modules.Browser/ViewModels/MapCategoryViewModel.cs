﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Core;
using SaberStudio.Core.Extensions;
using SaberStudio.Modules.Browser.Views;
using SaberStudio.Services.BeatSaver.Interfaces;
using SaberStudio.Services.BeatSaver.Parser.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace SaberStudio.Modules.Browser.ViewModels
{
    public class MapCategoryViewModel : BindableBase, INavigationAware
    {
        private string navigationPath;
        private readonly IRegionManager regionManager;

        private DelegateCommand<BeatMap> selectedCommand;
        public DelegateCommand<BeatMap> SelectedCommand => selectedCommand ??= new DelegateCommand<BeatMap>(ExecuteSelectedCommand);

        private DelegateCommand<BeatMap> downloadCommand;
        public DelegateCommand<BeatMap> DownloadCommand => downloadCommand ??= new DelegateCommand<BeatMap>(ExecuteDownloadCommand);

        private void ExecuteDownloadCommand(BeatMap map)
        {
            beatSaverClient.DownloadMap(CancellationToken.None, map);
        }

        private void ExecuteSelectedCommand(BeatMap map)
        {
            var navParams = new NavigationParameters
                {
                    { "BeatMap",  map }
                };

            regionManager.RequestNavigate(Regions.ContentRegion, nameof(MapDetailView), navParams);
        }

        private int currentPage = 1;
        public int CurrentPage
        {
            get => currentPage;
            set => SetProperty(ref currentPage, value);
        }

        private readonly IBeatSaverClient beatSaverClient;
        private ObservableCollection<BeatMap> beatMaps;
        public ObservableCollection<BeatMap> BeatMaps
        {
            get => beatMaps;
            set => SetProperty(ref beatMaps, value);
        }

        public MapCategoryViewModel(IBeatSaverClient beatSaverClient, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.beatSaverClient = beatSaverClient;
            BeatMaps = new ObservableCollection<BeatMap>();
        }

        private DelegateCommand goToPreviousPageCommand;
        public DelegateCommand GoToPreviousPageCommand => goToPreviousPageCommand ??= new DelegateCommand(ExecuteGoToPreviousPageCommand, CanGoBack);

        private bool CanGoBack()
        {
            return CurrentPage > 0;
        }

        private void ExecuteGoToPreviousPageCommand()
        {
            CurrentPage--;
            GetMaps();
            GoToPreviousPageCommand.RaiseCanExecuteChanged();
            GoToNextPageCommand.RaiseCanExecuteChanged();
        }

        private DelegateCommand goToNextPageCommand;
        public DelegateCommand GoToNextPageCommand => goToNextPageCommand ??= new DelegateCommand(ExecuteGoToNextPageCommand);

        private void ExecuteGoToNextPageCommand()
        {
            CurrentPage++;
            GetMaps();
            GoToPreviousPageCommand.RaiseCanExecuteChanged();
            GoToNextPageCommand.RaiseCanExecuteChanged();
        }

        private async void GetMaps()
        {
            IEnumerable<BeatMap> maps = new List<BeatMap>();

            if (navigationPath == NavigationPaths.LatestMaps.ToString())
                maps = await beatSaverClient.GetLatestMaps(CancellationToken.None, CurrentPage - 1);

            if (navigationPath == NavigationPaths.MostDownloadedMaps.ToString())
                maps = await beatSaverClient.GetMostDownloadedMaps(CancellationToken.None, CurrentPage - 1);

            if (navigationPath == NavigationPaths.MostPlayedMaps.ToString())
                maps = await beatSaverClient.GetMostPlayedMaps(CancellationToken.None, CurrentPage - 1);

            if (navigationPath == NavigationPaths.TopRatedMaps.ToString())
                maps = await beatSaverClient.GetTopRatedMaps(CancellationToken.None, CurrentPage - 1);

            if (navigationPath == NavigationPaths.TrendingMaps.ToString())
                maps = await beatSaverClient.GetTrendingMaps(CancellationToken.None, CurrentPage - 1);

            BeatMaps.Clear();
            BeatMaps.AddRange(maps.ToObservableCollection());
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return navigationPath == navigationContext.Parameters["NavigationPath"] as string;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            navigationPath = navigationContext.Parameters["NavigationPath"] as string;
            GetMaps();
        }
    }
}
