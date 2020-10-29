using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Core;
using SaberStudio.Core.Extensions;
using SaberStudio.Modules.Browser.Views;
using SaberStudio.Services.BeatMods.Interfaces;
using SaberStudio.Services.BeatMods.Models;
using SaberStudio.Services.BeatMods.Models.Parser;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using static SaberStudio.Services.BeatMods.Models.Constants;

namespace SaberStudio.Modules.Browser.ViewModels
{
    public class ModBrowserViewModel : BindableBase, INavigationAware
    {
        private readonly IBeatModsClient beatModsClient;
        private readonly IRegionManager regionManager;


        private ObservableCollection<Mod> mods;
        public ObservableCollection<Mod> Mods
        {
            get { return mods; }
            set { SetProperty(ref mods, value); }
        }

        private DelegateCommand<Mod> selectedCommand;
        public DelegateCommand<Mod> SelectedCommand => selectedCommand ??= new DelegateCommand<Mod>(ExecuteSelectedCommand);

        private void ExecuteSelectedCommand(Mod mod)
        {
            var navParams = new NavigationParameters
                {
                    { "Mod",  mod }
                };

            regionManager.RequestNavigate(Regions.ContentRegion, typeof(ModDetailView).Name, navParams);
        }

        private int currentPage = 1;
        public int CurrentPage
        {
            get { return currentPage; }
            set { SetProperty(ref currentPage, value); }
        }

        public ModBrowserViewModel(IBeatModsClient beatModsClient, IRegionManager regionManager)
        {
            this.beatModsClient = beatModsClient;
            this.regionManager = regionManager;
            Mods = new ObservableCollection<Mod>();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private async void GetMods()
        {
            var mods = await beatModsClient.GetMods(CancellationToken.None, GameVersion.V1_12_2, "", "", SortOrder.Descending, Status.Approved);
            Mods.Clear();
            Mods.AddRange(mods.ToObservableCollection());
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetMods();
        }
    }
}
