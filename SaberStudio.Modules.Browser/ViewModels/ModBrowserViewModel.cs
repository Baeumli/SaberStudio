using System.Collections.Generic;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Modules.Browser.Views;
using SaberStudio.Services.BeatMods.Interfaces;
using SaberStudio.Services.BeatMods.Models;
using SaberStudio.Services.BeatMods.Models.Parser;
using SaberStudio.Services.BeatSaber;
using SaberStudio.UI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Prism.Services.Dialogs;
using SaberStudio.UI.Dialogs.Views;

namespace SaberStudio.Modules.Browser.ViewModels
{
    public class ModBrowserViewModel : BindableBase, INavigationAware
    {
        private readonly IBeatModsClient beatModsClient;
        private readonly IBeatSaberService beatSaberService;
        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;

        #region Properties 

        public ObservableCollection<Mod> Mods { get; set; } = new ObservableCollection<Mod>();

        private Mod selectedMod;
        public Mod SelectedMod
        {
            get => selectedMod;
            set => SetProperty(ref selectedMod, value);
        }

        private string gameVersion;
        public string GameVersion       
        {
            get => gameVersion;
            set => SetProperty(ref gameVersion, value);
        }

        #endregion

        public ModBrowserViewModel(IRegionManager regionManager, IDialogService dialogService, IBeatModsClient beatModsClient, IBeatSaberService beatSaberService)
        {
            this.regionManager = regionManager;
            this.dialogService = dialogService;
            this.beatModsClient = beatModsClient;
            this.beatSaberService = beatSaberService;
        }

        #region Commands 

        private DelegateCommand selectedCommand;
        public DelegateCommand SelectedCommand => selectedCommand ??= new DelegateCommand(ExecuteSelectedCommand);
        private void ExecuteSelectedCommand()
        {
            var navParams = new NavigationParameters
            {
                { "Mod",  SelectedMod }
            };

            regionManager.RequestNavigate(Regions.ContentRegion, nameof(ModDetailView), navParams);
        }

        private DelegateCommand downloadCommand;
        public DelegateCommand DownloadCommand => downloadCommand ??= new DelegateCommand(ExecuteDownloadCommand);

        private async void ExecuteDownloadCommand()
        {
            var currentGameVersion = beatSaberService.GetGameVersion();
            if (currentGameVersion != SelectedMod.GameVersion)
            {
                if (!ShowDialog())
                    return;
            }

            await beatModsClient.DownloadMod(CancellationToken.None, SelectedMod);
        }

        #endregion
        
        private async Task<IEnumerable<Mod>> GetMods()
        {
            return await beatModsClient.GetMods(CancellationToken.None, GameVersion, "", "", SortOrder.Descending, Constants.Status.Approved);
        }

        private async void InstallModLoader()
        {
            await beatModsClient.InstallModLoader(GameVersion);
        }

        private async Task<string> GetGameVersion()
        {
            var version = beatSaberService.GetGameVersion();
            var alias = await beatModsClient.GetGameVersionFromAlias(version, CancellationToken.None);
            return string.IsNullOrEmpty(alias) ? version : alias;
        }
        

        #region Navigation 

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
                        
            GameVersion = await GetGameVersion();
            await beatModsClient.CheckInstalledMods(GameVersion);

            InstallModLoader();
            
            var mods = await GetMods();
            Mods.Clear();
            Mods.AddRange(mods.Where((x => x.IsRequired == false)));
        }

        private bool ShowDialog()
        {
            var resume = false;

            dialogService.ShowDialog(nameof(VersionMismatchDialog), result =>
            {
                switch (result.Result)
                {
                    case ButtonResult.Yes:
                        resume = true;
                        break;
                    case ButtonResult.No:
                        resume = false;
                        break;
                    default:
                        return;
                }
            });
            return resume;
        }

        #endregion

    }
}
