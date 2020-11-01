using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Core;
using SaberStudio.Domain.Models.Components;
using SaberStudio.Modules.Browser.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaberStudio.Modules.Browser.ViewModels
{
    public class MapBrowserViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;

        public List<Tile> Categories { get; set; }

        public MapBrowserViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            Categories = new List<Tile>()
            {
                new Tile()
                {
                    Content = "Trending",
                    TargetView = nameof(MapCategoryView),
                    NavigationPath = NavigationPaths.TrendingMaps.ToString()
                },
                new Tile()
                {
                    Content = "Top Rated",
                    TargetView = nameof(MapCategoryView),
                    NavigationPath = NavigationPaths.TopRatedMaps.ToString()
                },
                new Tile()
                {
                    Content = "Latest",
                    TargetView = nameof(MapCategoryView),
                    NavigationPath = NavigationPaths.LatestMaps.ToString()
                },
                new Tile()
                {
                    Content = "Most Downloaded",
                    TargetView = nameof(MapCategoryView),
                    NavigationPath = NavigationPaths.MostDownloadedMaps.ToString()
                },
                new Tile()
                {
                    Content = "Most Played",
                    TargetView = nameof(MapCategoryView),
                    NavigationPath = NavigationPaths.MostPlayedMaps.ToString()
                }
            };
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
        }

        private DelegateCommand<Tile> selectedCommand;

        public DelegateCommand<Tile> SelectedCommand => selectedCommand ??= new DelegateCommand<Tile>(ExecuteSelectedCommand);

        private void ExecuteSelectedCommand(Tile tile)
        {
            if (string.IsNullOrEmpty(tile.TargetView))
                throw new ArgumentNullException();

            var activeView = regionManager.Regions[Regions.ContentRegion].ActiveViews.FirstOrDefault();

            if (nameof(activeView) == tile.TargetView) return;
            var navParams = new NavigationParameters
            {
                { "NavigationPath",  tile.NavigationPath }
            };

            regionManager.RequestNavigate(Regions.ContentRegion, tile.TargetView, navParams);
        }
    }
}
