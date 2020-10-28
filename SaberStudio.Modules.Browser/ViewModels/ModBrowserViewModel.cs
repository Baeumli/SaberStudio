using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Core.Extensions;
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

        private ObservableCollection<Mod> mods;
        public ObservableCollection<Mod> Mods
        {
            get { return mods; }
            set { SetProperty(ref mods, value); }
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
